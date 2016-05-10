using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using mercadopago;
using Microsoft.Ajax.Utilities;
using Model.Entities;
using System.Net;
using System.Web.Http;
using Multipay.DTOs;
using Services;
using RestSharp;

namespace Multipay.Controllers
{
    public class LoginController : ApiController
    {
        private UserService UserService = new UserService();

        private static readonly Encoding Encoding = Encoding.UTF8; 

        [HttpPost]
        [Route("api/attemptNativeLogin")]
        public LoginResponseDTO AttemptNativeLogin(LoginRequestDTO loginRequest)
        {
            var loginValid = false;
            var userId = -1;
            var message = "";
            var userName = "";
            var userEmail = "";
            var password = loginRequest.Password;

            var user = UserService.GetByEmail(loginRequest.Email, loginRequest.IsSeller);
            if (user != null && password != null)
            {

                if (loginRequest.Password.Equals(user.Password))
                {
                    if (user.Active)
                    {
                        var device = user.Device;
                        device.MobileId = loginRequest.MobileId;
                        device.RegistrationId = loginRequest.RegistrationId;
                        UserService.Update(user);

                        loginValid = true;
                        userId = user.Id;
                        userName = user.Name;
                        userEmail = user.Email;
                    }
                    else
                    {
                        message = "Usuario inactivo.";
                    }
                }
                else
                {
                    message = "Usuario / contraseña incorrecta.";
                }

            }
            else
            {
                message = "Usuario / contraseña incorrecta.";
            }

            var loginResponse = new LoginResponseDTO
            {
                Valid = loginValid,
                Message = message,
                UserId = userId,
                UserName = userName,
                UserEmail = userEmail
            };

            return loginResponse;
        }

        [HttpPost]
        [Route("api/googleTokenInfo")]
        public LoginResponseDTO GoogleTokenInfo(LoginRequestDTO loginRequest)
        {
            var loginValid = false;
            var userId = -1;
            var message = "";
            var userName = "";
            var userEmail = "";

            var client = new RestClient(ConfigurationManager.AppSettings["GOOGLE_API_BASE_URL"]);

            var request = new RestRequest("/oauth2/v3/tokeninfo", Method.GET);
            request.AddParameter("id_token", loginRequest.SocialToken); // adds to POST or URL querystring based on Method

            var response = (RestResponse<GoogleTokenInfoDTO>) client.Execute<GoogleTokenInfoDTO>(request);
            
            // si da OK la peticion a Google
            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
            {
                var googleTokenInfoDto = response.Data;
                // Si el parametro Aud es igual al OauthClientId para el que fue generado en Android esta Ok.
                if (googleTokenInfoDto.Aud == GoogleTokenInfoDTO.GoogleOauthServerClientId)
                {
                    var id = googleTokenInfoDto.Sub;
                    var email = googleTokenInfoDto.Email;
                    var firstName = googleTokenInfoDto.GivenName;
                    var lastName = googleTokenInfoDto.FamilyName;

                    var user = UserService.GetByEmail(email, loginRequest.IsSeller);
                    if (user == null)
                    {
                        if (loginRequest.IsSeller)
                        {
                            var seller = new Seller
                            {
                                Email = email,
                                Name = firstName,
                                Date = DateTime.Now,
                                Active = true,
                                Device = new Device
                                {
                                    MobileId = loginRequest.MobileId,
                                    RegistrationId = loginRequest.RegistrationId
                                },
                                SocialNetworkId = id
                            };
                            UserService.Save(seller);
                        }
                        else
                        {
                            var mp = new MP(ConfigurationManager.AppSettings["MPAccessToken"]);
                            mp.sandboxMode(true);
                            
                            // Me fijo si ya existia ese Customer para ese email.
                            var filters = new Dictionary<String, String> {{"email", email}};
                            var customerSearch = mp.get("/v1/customers/search", filters);
                            var customerSearchResponse = (Hashtable)customerSearch["response"];
                            var results = (ArrayList)customerSearchResponse["results"];
                            var resultsHashtable = (Hashtable)results[0];
                            var customerId = (string)resultsHashtable["id"];
                            if (customerId.IsNullOrWhiteSpace())
                            {
                                var customerSaved = mp.post("/v1/customers", "{\"email\": \"" + email + "\"}");
                                customerSearchResponse = (Hashtable)customerSaved["response"];
                                results = (ArrayList)customerSearchResponse["results"];
                                resultsHashtable = (Hashtable)results[0];
                                customerId = (string)resultsHashtable["id"];                                
                            }
                            var buyer = new Buyer
                            {
                                Email = email,
                                Name = firstName,
                                LastName = lastName,
                                Date = DateTime.Now,
                                Active = true,
                                Device = new Device
                                {
                                    MobileId = loginRequest.MobileId,
                                    RegistrationId = loginRequest.RegistrationId
                                },
                                SocialNetworkId = id,
                                MPCustomerId = (string) customerId
                            };
                            UserService.Save(buyer);
                        }
                        loginValid = true;
                        userName = googleTokenInfoDto.Name;
                        userEmail = email;
                    }
                    else
                    {
                        if (user.Active)
                        {
                            var device = user.Device;
                            device.MobileId = loginRequest.MobileId;
                            device.RegistrationId = loginRequest.RegistrationId;
                            UserService.Update(user);

                            loginValid = true;
                            userId = user.Id;
                            userName = user.Name;
                            userEmail = user.Email;
                        }
                        else
                        {
                            message = "Usuario inactivo.";
                        }
                    }
                }
                else
                {
                    message = "ERROR - NO COINCIDE EL OAUTH CLIENT ID PARA EL QUE FUE GENERADO EL TOKEN";
                }
            }
            else
            {
                message = "ERROR - API Google";
            }

            var loginResponse = new LoginResponseDTO
            {
                Valid = loginValid,
                Message = message,
                UserId = userId,
                UserName = userName,
                UserEmail = userEmail
            };

            return loginResponse;
        }

        [HttpPost]
        [Route("api/facebookTokenInfo")]
        public LoginResponseDTO FacebookTokenInfo(LoginRequestDTO loginRequest)
        {
            var loginValid = false;
            var userId = -1;
            var message = "";
            var userName = "";
            var userEmail = "";

            var client = new RestClient(ConfigurationManager.AppSettings["FACEBOKK_API_BASE_URL"]);

            var keyByte = Encoding.GetBytes(ConfigurationManager.AppSettings["FACEBOOK_APP_SECRET_PROOF"]);
            var hmacsha256 = new HMACSHA256(keyByte);
            byte[] hashedBytes = hmacsha256.ComputeHash(Encoding.GetBytes(loginRequest.SocialToken));

            var output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            var request = new RestRequest("/me", Method.GET);
            request.AddParameter("access_token", loginRequest.SocialToken); // adds to POST or URL querystring based on Method
            request.AddParameter("appsecret_proof", output.ToString());

            var response = (RestResponse<FacebookTokenInfoDTO>)client.Execute<FacebookTokenInfoDTO>(request);
            // si da OK la peticion a Google
            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
            {
                var facebookTokenInfoDto = response.Data;
                var id = facebookTokenInfoDto.Id;
                var email = facebookTokenInfoDto.Email;
                var firstName = facebookTokenInfoDto.FirstName;
                var lastName = facebookTokenInfoDto.LastName;

                var user = UserService.GetByEmail(email, loginRequest.IsSeller);
                if (user == null)
                {
                    if (loginRequest.IsSeller)
                    {
                        var seller = new Seller
                        {
                            Email = email,
                            Name = firstName,
                            Date = DateTime.Now,
                            Active = true,
                            Device = new Device
                            {
                                MobileId = loginRequest.MobileId,
                                RegistrationId = loginRequest.RegistrationId
                            },
                            SocialNetworkId = id
                        };
                        UserService.Save(seller);
                    }
                    else
                    {
                        var mp = new MP(ConfigurationManager.AppSettings["MPAccessToken"]);
                        mp.sandboxMode(true);

                        // Me fijo si ya existia ese Customer para ese email.
                        var filters = new Dictionary<String, String> { { "email", email } };
                        var customerSearch = mp.get("/v1/customers/search", filters);
                        var customerSearchResponse = (Hashtable)customerSearch["response"];
                        var results = (ArrayList)customerSearchResponse["results"];
                        var resultsHashtable = (Hashtable)results[0];
                        var customerId = (string)resultsHashtable["id"];
                        if (customerId.IsNullOrWhiteSpace())
                        {
                            var customerSaved = mp.post("/v1/customers", "{\"email\": \"" + email + "\"}");
                            customerSearchResponse = (Hashtable)customerSaved["response"];
                            results = (ArrayList)customerSearchResponse["results"];
                            resultsHashtable = (Hashtable)results[0];
                            customerId = (string)resultsHashtable["id"];
                        }
                        var buyer = new Buyer
                        {
                            Email = email,
                            Name = firstName,
                            LastName = lastName,
                            Date = DateTime.Now,
                            Active = true,
                            Device = new Device
                            {
                                MobileId = loginRequest.MobileId,
                                RegistrationId = loginRequest.RegistrationId
                            },
                            SocialNetworkId = id,
                            MPCustomerId = (string)customerId
                        };
                        UserService.Save(buyer);
                    }
                    loginValid = true;
                    userName = facebookTokenInfoDto.Name;
                    userEmail = email;
                }
                else
                {
                    if (user.Active)
                    {
                        var device = user.Device;
                        device.MobileId = loginRequest.MobileId;
                        device.RegistrationId = loginRequest.RegistrationId;
                        UserService.Update(user);

                        loginValid = true;
                        userId = user.Id;
                        userName = user.Name;
                        userEmail = user.Email;
                    }
                    else
                    {
                        message = "Usuario inactivo.";
                    }
                }
            }
            else
            {
                message = "ERROR - API Facebook";
            }

            var loginResponse = new LoginResponseDTO
            {
                Valid = loginValid,
                Message = message,
                UserId = userId,
                UserName = userName,
                UserEmail = userEmail
            };
            return loginResponse;
        }
    }
}
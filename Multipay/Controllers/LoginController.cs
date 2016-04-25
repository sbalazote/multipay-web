using System.Security.Cryptography;
using System.Text;
using Model.Entities;
using Multipay.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Multipay.DTOs;
using Services;
using RestSharp;

namespace Multipay.Controllers
{
    public class LoginController : ApiController
    {
        private UserService UserService = new UserService();

        private const string AppSecretProof = "657df0797fbbec4aa19c24a12b2cc4bc";

        private static readonly Encoding Encoding = Encoding.UTF8; 

        [HttpPost]
        public LoginResponseDTO AttemptLogin(LoginRequestDTO loginRequest)
        {
            LoginResponseDTO loginResponse = new LoginResponseDTO();
            loginResponse.Valid = true;
            loginResponse.Message = "Ok";
            loginResponse.UserId = 1;
            loginResponse.Username = "";

            return loginResponse;
            /*LoginResponseDTO loginResponse = null;
            Boolean loginValid = false;
            int userId = -1;
            String message = "";
            String userName = "";
            String password = loginRequest.Password;

            User user = UserService.GetByEmail(loginRequest.Email);
            if (user != null && password != null)
            {

                if (loginRequest.Password.Equals(user.Password))
                {
                    if (user.Active)
                    {

                        loginValid = true;
                        userId = user.Id;
                        userName = user.Name;
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

            loginResponse = new LoginResponseDTO();
            loginResponse.Valid = loginValid;
            loginResponse.Message = message;
            loginResponse.UserId = userId;
            loginResponse.Username = userName;

            return loginResponse;*/
        }

        [HttpPost]
        [Route("api/googleTokenInfo")]
        public string GoogleTokenInfo([FromBody] string tokenId)
        {
            var client = new RestClient("https://www.googleapis.com");

            var request = new RestRequest("/oauth2/v3/tokeninfo", Method.GET);
            request.AddParameter("id_token", tokenId); // adds to POST or URL querystring based on Method

            var response = (RestResponse<GoogleTokenInfoDTO>) client.Execute<GoogleTokenInfoDTO>(request);
            
            // si da OK la peticion a Google
            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
            {
                GoogleTokenInfoDTO googleTokenInfoDto = response.Data;
                // Si el parametro Aud es igual al OauthClientId para el que fue generado en Android esta Ok.
                if (googleTokenInfoDto.Aud == GoogleTokenInfoDTO.GoogleOauthServerClientId)
                {
                    // TODO intentar autenticar usuario con sus parametros.
                    var Id = googleTokenInfoDto.Sub;
                    var Email = googleTokenInfoDto.Email;
                    var FirstName = googleTokenInfoDto.GivenName;
                    var LastName = googleTokenInfoDto.FamilyName;
                }
                else
                {
                    // TODO ERROR NO COINCIDE EL OAUTH CLIENT ID PARA EL QUE FUE GENERADO EL TOKEN
                }
            }
            else
            {
                // TODO ERROR API Google
            }

            return "Ok";
        }

        [HttpPost]
        [Route("api/facebookTokenInfo")]
        public string FacebookTokenInfo([FromBody] string accessToken)
        {
            var client = new RestClient("https://graph.facebook.com");

            var keyByte = Encoding.GetBytes(AppSecretProof);
            var hmacsha256 = new HMACSHA256(keyByte);
            byte[] hashedBytes = hmacsha256.ComputeHash(Encoding.GetBytes(accessToken));

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            var request = new RestRequest("/me", Method.GET);
            request.AddParameter("access_token", accessToken); // adds to POST or URL querystring based on Method
            request.AddParameter("appsecret_proof", output.ToString());

            var response = (RestResponse<FacebookTokenInfoDTO>)client.Execute<FacebookTokenInfoDTO>(request);
            // si da OK la peticion a Google
            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
            {
                FacebookTokenInfoDTO facebookTokenInfoDto = response.Data;
                // TODO intentar autenticar usuario con sus parametros.
                var Id = facebookTokenInfoDto.Id;
                var Email = facebookTokenInfoDto.Email;
                var FirstName = facebookTokenInfoDto.FirstName;
                var LastName = facebookTokenInfoDto.LastName;
            }
            else
            {
                // TODO ERROR API Facebook
            }

            return "Ok";
        }

        static string ByteToString(byte[] buff)
        {
            string sbinary = "";
            for (int i = 0; i < buff.Length; i++)
                sbinary += buff[i].ToString("X2"); /* hex format */
            return sbinary;
        }  
    }
}

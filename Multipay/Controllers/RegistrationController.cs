using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using mercadopago;
using Microsoft.Ajax.Utilities;
using Model.Entities;
using Multipay.DTOs;
using Services;


namespace Multipay.Controllers
{
    public class RegistrationController : ApiController
    {
        private UserService UserService = new UserService();

        [HttpPost]
        [Route("api/attemptNativeRegistration")]
        public LoginResponseDTO AttemptNativeRegistration(RegistrationRequestDTO registrationRequest)
        {
            var loginValid = false;
            var userId = -1;
            var message = "";
            var userName = "";
            var userEmail = "";
            var password = registrationRequest.Password;

            var user = UserService.GetByEmail(registrationRequest.Email, registrationRequest.IsSeller);
            if (user == null)
            {
                if (registrationRequest.IsSeller)
                {
                    var seller = new Seller
                    {
                        Email = registrationRequest.Email,
                        Name = registrationRequest.Name,
                        Date = DateTime.Now,
                        Password = password,
                        Active = true,
                        Device = new Device
                        {
                            MobileId = registrationRequest.MobileId,
                            RegistrationId = registrationRequest.RegistrationId
                        },
                    };
                    UserService.Save(seller);
                }
                else
                {
                    /*var mp = new MP(ConfigurationManager.AppSettings["MPAccessToken"]);

                    // Me fijo si ya existia ese Customer para ese email.
                    var filters = new Dictionary<String, String> { { "email", registrationRequest.Email } };
                    var customerSearch = mp.get("/v1/customers/search", filters);
                    var customerSearchResponse = (Hashtable)customerSearch["response"];
                    var results = (ArrayList)customerSearchResponse["results"];
                    string customerId;
                    // No existe el Customer para ese mail.
                    if (results.Count == 0)
                    {
                        var customerSaved = mp.post("/v1/customers", "{\"email\": \"" + registrationRequest.Email + "\"}");
                        customerSearchResponse = (Hashtable) customerSaved["response"];
                        customerId = (string)customerSearchResponse["id"];
                    }
                    // Existe el Customer para ese mail.
                    else
                    {
                        Hashtable resultsHashtable = (Hashtable)results[0];
                        customerId = (string)resultsHashtable["id"];
                        if (customerId.IsNullOrWhiteSpace())
                        {
                            var customerSaved = mp.post("/v1/customers", "{\"email\": \"" + registrationRequest.Email + "\"}");
                            customerSearchResponse = (Hashtable)customerSaved["response"];
                            results = (ArrayList)customerSearchResponse["results"];
                            resultsHashtable = (Hashtable)results[0];
                            customerId = (string)resultsHashtable["id"];
                        }
                    }*/
                    
                    var identification = new Identification
                    {
                        Type = registrationRequest.IdentificationType,
                        Number = registrationRequest.IdentificationNumber
                    };
                    var address = new Address
                    {
                        Name = registrationRequest.Name,
                        Number = registrationRequest.AddressNumber,
                        ZipCode = registrationRequest.AddressZipCode
                    };
                    var phone = new Phone
                    {
                        AreaCode = registrationRequest.PhoneAreaCode,
                        Number = registrationRequest.PhoneNumber
                    };
                    var buyer = new Buyer
                    {
                        Email = registrationRequest.Email,
                        Name = registrationRequest.Name,
                        Date = DateTime.Now,
                        Password = registrationRequest.Password,
                        Active = true,
                        Device = new Device
                        {
                            MobileId = registrationRequest.MobileId,
                            RegistrationId = registrationRequest.RegistrationId
                        },
                        //MPCustomerId = (string)customerId,
                        LastName = registrationRequest.LastName,
                        Identification = identification,
                        Address = address,
                        Phone = phone
                    };
                    UserService.Save(buyer);
                }
                loginValid = true;
                userName = registrationRequest.Name;
                userEmail = registrationRequest.Email;

            }
            else
            {
                message = "Error - Usuario existente.";
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
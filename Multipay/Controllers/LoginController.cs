using Model.Entities;
using Multipay.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Services;

namespace Multipay.Controllers
{
    public class LoginController : ApiController
    {
        private UserService UserService = new UserService();

        [HttpPost]
        public LoginResponseDTO AttemptLogin(LoginRequestDTO loginRequest)
        {
            LoginResponseDTO loginResponse = null;
            Boolean loginValid = false;
            int userId = -1;
            String message = "";
            String userName = "";
            String password = loginRequest.UserPassword;

            User user = UserService.GetByEmail(loginRequest.UserEmail);
            if (user != null && password != null)
            {

                if (loginRequest.UserPassword.Equals(user.Password))
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

            return loginResponse;
        }
    }
}

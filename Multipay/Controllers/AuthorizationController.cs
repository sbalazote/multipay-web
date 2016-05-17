using System.Configuration;
using log4net;
using Model.Entities;
using Multipay.DTOs;
using System;
using System.Net;
using System.Reflection;
using System.Web.Http;
using RestSharp;
using Services;

namespace Multipay.Controllers
{
    public class AuthorizationController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private UserService UserService = new UserService();

        [HttpGet]
        [Route("api/authorization")]
        public bool GetAuthorizationCode(string code, string email)
        {
            // obtengo el usuario por el mail
            var user = (Seller)UserService.GetByEmail(email, true);

            var client = new RestClient(ConfigurationManager.AppSettings["MP_API_BASE_URL"]);

            var request = new RestRequest("/oauth/token", Method.POST);
            request.AddParameter("client_secret", ConfigurationManager.AppSettings["MPAccessToken"]);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("code", code);
            request.AddParameter("redirect_uri", ConfigurationManager.AppSettings["OUR_BASE_URL"] + "/api/authorization?email=" + email);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            var response = (RestResponse<AuthorizationTokenDTO>)client.Execute<AuthorizationTokenDTO>(request);

            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
            {
                var authorizationTokenDto = response.Data;

                var token = new Token
                {
                    AccessToken = authorizationTokenDto.AccessToken,
                    Expiration = authorizationTokenDto.ExpiresIn,
                    RefreshToken = authorizationTokenDto.RefreshToken,
                    PublicKey = authorizationTokenDto.PublicKey,
                    RequestedTime = DateTime.Now,
                    Scope = authorizationTokenDto.Scope,
                    Type = authorizationTokenDto.TokenType
                };
                user.Token = token;
                user.MPSellerUserId = authorizationTokenDto.UserId;
                UserService.Update(user);
                return true;
            }
            return false;
        }
    }
}
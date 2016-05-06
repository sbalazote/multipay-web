using System.Security.Cryptography;
using System.Text;
using Multipay.DTO;
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

        private const string AppSecretProof = "657df0797fbbec4aa19c24a12b2cc4bc";

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

            var user = UserService.GetByEmail(loginRequest.Email);
            if (user != null && password != null)
            {

                if (loginRequest.Password.Equals(user.Password))
                {
                    if (user.Active)
                    {

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
        public LoginResponseDTO GoogleTokenInfo([FromBody] string tokenId)
        {
            var loginValid = false;
            var userId = -1;
            var message = "";
            var userName = "";
            var userEmail = "";

            var client = new RestClient("https://www.googleapis.com");

            var request = new RestRequest("/oauth2/v3/tokeninfo", Method.GET);
            request.AddParameter("id_token", tokenId); // adds to POST or URL querystring based on Method

            var response = (RestResponse<GoogleTokenInfoDTO>) client.Execute<GoogleTokenInfoDTO>(request);
            
            // si da OK la peticion a Google
            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
            {
                var googleTokenInfoDto = response.Data;
                // Si el parametro Aud es igual al OauthClientId para el que fue generado en Android esta Ok.
                if (googleTokenInfoDto.Aud == GoogleTokenInfoDTO.GoogleOauthServerClientId)
                {
                    // TODO intentar autenticar usuario con sus parametros.
                    var id = googleTokenInfoDto.Sub;
                    var email = googleTokenInfoDto.Email;
                    var firstName = googleTokenInfoDto.GivenName;
                    var lastName = googleTokenInfoDto.FamilyName;

                    loginValid = true;
                    // TODO setear el id de la base si es que existe.
                    userName = firstName;
                    userEmail = email;
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
        public LoginResponseDTO FacebookTokenInfo([FromBody] string accessToken)
        {
            var loginValid = false;
            var userId = -1;
            var message = "";
            var userName = "";
            var userEmail = "";

            var client = new RestClient("https://graph.facebook.com");

            var keyByte = Encoding.GetBytes(AppSecretProof);
            var hmacsha256 = new HMACSHA256(keyByte);
            byte[] hashedBytes = hmacsha256.ComputeHash(Encoding.GetBytes(accessToken));

            var output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            var request = new RestRequest("/me", Method.GET);
            request.AddParameter("access_token", accessToken); // adds to POST or URL querystring based on Method
            request.AddParameter("appsecret_proof", output.ToString());

            var response = (RestResponse<FacebookTokenInfoDTO>)client.Execute<FacebookTokenInfoDTO>(request);
            // si da OK la peticion a Google
            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
            {
                var facebookTokenInfoDto = response.Data;
                // TODO intentar autenticar usuario con sus parametros.
                var id = facebookTokenInfoDto.Id;
                var email = facebookTokenInfoDto.Email;
                var firstName = facebookTokenInfoDto.FirstName;
                var lastName = facebookTokenInfoDto.LastName;

                loginValid = true;
                // TODO setear el id de la base si es que existe.
                userName = firstName;
                userEmail = email;
            }
            else
            {
                // TODO ERROR API Facebook
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

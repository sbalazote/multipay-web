using log4net;
using Model.Entities;
using Multipay.DTOs;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web.Http;
using Services;

namespace Multipay.Controllers
{
    public class AuthorizationController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private UserService UserService = new UserService();
        
        public string GetAuthorizationCode(string code, string email) {

            Log.Debug("AuthorizationController.cs" + "   code: " + code + "email: " + email);

            // obtengo el usuario por el mail
            User User = UserService.GetByEmail(email);

            string GrantType = "authorization_code";
            string ClientId = "3108634673635661";
            string ClientSecret = "4qCgUNhtRA3BnMJlAOjoXFZLT2EobUWJ";
            string Code = code;
            string RedirectURI = "http://multipay.ddns.net:8080/api/authorization";
            
            string postString = string.Format("grant_type={0}&client_id={1}&client_secret={2}&code={3}&redirect_uri={4}", GrantType, ClientId, ClientSecret, Code, RedirectURI);
            
            HttpWebRequest request = WebRequest.Create("https://api.mercadolibre.com/oauth/token") as HttpWebRequest;
            request.Method = "POST";
            request.ContentLength = postString.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "application/json";

            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            byte[] byteArray = Encoding.UTF8.GetBytes(postString);
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse ();
            // Display the status.
            Console.WriteLine (((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream ();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader (dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd ();
            // Display the content.
            Console.WriteLine (responseFromServer);
            // Clean up the streams.
            reader.Close ();
            dataStream.Close ();
            response.Close ();

            //TODO guardar los datos de tokens en el usuario.
            AuthorizationTokenDTO AuthToken = JsonConvert.DeserializeObject<AuthorizationTokenDTO>(responseFromServer);
            User.AccessToken = AuthToken.AccessToken;
            User.TokenExpires = AuthToken.ExpiresIn;
            User.RefreshToken = AuthToken.RefreshToken;
            UserService.Save(User);

            return AuthToken.AccessToken;
        }
    }
}

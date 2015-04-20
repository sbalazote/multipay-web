using log4net;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web.Http;
using Model.Entities;
using Services;

namespace Multipay.Controllers
{
    public class AuthenticationController : ApiController
    {
        private UserService UserService = new UserService();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public string GetAuthenticationToken(string email)
        {
            //TODO checkear en la db si el tiempo expiro para el token, si es asi pedir uno nuevo. select x el email del seller.
            var user = UserService.GetByEmail(email);
            var actualDT = new DateTime();
            var tokenRequestedDT =((Seller)user).Token.RequestedTime;
            
            //token expiro
            if (actualDT.CompareTo(tokenRequestedDT.AddSeconds(((Seller)user).Token.Expiration)) > 0)
            {
                string GrantType = "refresh_token";
                string ClientId = "3108634673635661";
                string ClientSecret = "4qCgUNhtRA3BnMJlAOjoXFZLT2EobUWJ";
                string RefreshToken = ((Seller)user).Token.RefreshToken;
                string postString = string.Format("grant_type={0}&client_id={1}&client_secret={2}&refresh_token={3}", GrantType, ClientId, ClientSecret, RefreshToken);

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
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
                Log.Debug(responseFromServer);

                return responseFromServer;
            }

                //token no expiro, devuelvo el access token del user.
            else
            {
                return ((Seller)user).Token.AccessToken;
            }
        }
    }
}

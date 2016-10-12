using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Web;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using mercadopago;
using Model.Entities;
using Multipay.DTOs;
using Multipay.utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using Services;

namespace Multipay.Controllers
{
    public class NotificationController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly SellerService _sellerService = new SellerService();
        private readonly BuyerService _buyerService = new BuyerService();

        public void GetAuthenticationToken(string topic, int id) 
        {
            Log.Debug(topic);
            if (topic != null)
            {
            //return the created user
            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.Created));
            }
            else
            {
            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError));        
            }
        }

        [HttpPost]
        [Route("api/paymentLink")]
        public LoginResponseDTO PaymentLink([FromBody] PaymentLinkDTO paymentLinkDTO)
        {
            var loginValid = false;
            var userId = -1;
            var message = "";
            var userName = "";
            var userEmail = "";

            const string fcmNotificationMessageTitle = "¿Aceptar el Pago?";
            var fcmNotificationMessageText = "TOTAL $" + paymentLinkDTO.TransactionAmount + " " + paymentLinkDTO.Description;

            var buyer = _buyerService.GetByPhone(paymentLinkDTO.AreaCode, paymentLinkDTO.Number);
            if (buyer == null)
            {
                message = "No existe comprador para ese numero";
            }
            else
            {
                loginValid = true;
                userId = buyer.Id;
                userName = buyer.Name;
                userEmail = buyer.Email;

                var registrationToken = buyer.Device.RegistrationId;

                var client = new RestClient(ConfigurationManager.AppSettings["FCM_API_BASE_URL"]);

                var fcmRequest = new RestRequest("/fcm/send", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new RestSharpJsonNetSerializer()
                };
                fcmRequest.AddHeader("Content-Type", "application/json");
                fcmRequest.AddHeader("Authorization", "key=" + ConfigurationManager.AppSettings["FCM_SERVER_KEY"]);

                var fcmNotificationMessageDto = new FCMNotificationMessageDTO
                {
                    Data = new FCMNotificationMessageDTO.FCMNotificationMessageDetailDTO
                    {
                        Type = "paymentLink",
                        Title = fcmNotificationMessageTitle,
                        Text = fcmNotificationMessageText,
                        Description = paymentLinkDTO.Description,
                        SellerEmail = paymentLinkDTO.SellerEmail,
                        TransactionAmount = paymentLinkDTO.TransactionAmount.ToString(CultureInfo.InvariantCulture)
                    },
                    To = registrationToken
                };

                fcmRequest.AddJsonBody(fcmNotificationMessageDto);

                IRestResponse fcmResponse = client.Execute(fcmRequest);
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
        [Route("api/notifications")]
        public HttpResponseMessage Notifications([FromBody] WebhooksDTO webhooksDto)
        {
            var type = HttpContext.Current.Request.QueryString["type"];
            var dataId = HttpContext.Current.Request.QueryString["data.id"];

            var response = Request.CreateResponse(HttpStatusCode.Created);

            var fcmNotificationMessageTitle = "";
            var fcmNotificationMessageText = "";

            string registrationToken = null;
            if (type.Equals("payment"))
            {
                //  Obtengo los datos del pago creado y notifico al vendedor.
                if (webhooksDto.Action.Equals("payment.created"))
                {
                    var sellerId = webhooksDto.UserId;
                    var paymentId = webhooksDto.Data.Id;
                    var seller = _sellerService.GetByMPSellerUserId(sellerId);
                    var mp = new MP(seller.Token.AccessToken);
                    //  TODO cambiar el nombre a registrationToken en la db
                    registrationToken = seller.Device.RegistrationId;
                    var paymentDetail = mp.get("/v1/payments/" + paymentId, true);
                    var paymentDetails = ((Hashtable)paymentDetail["response"]);
                    var status = paymentDetails["status"];
                    var payerEmail = ((Hashtable)paymentDetails["payer"])["email"];
                    var description = paymentDetails["description"];
                    var transactionAmount = paymentDetails["transaction_amount"];
                    var dateLastUpdated = paymentDetails["date_last_updated"];
                    if (status.Equals("approved"))
                    {
                        fcmNotificationMessageTitle += "Pago APROBADO";
                    }
                    else if (status.Equals("rejected"))
                    {
                        fcmNotificationMessageTitle += "Pago RECHAZADO";
                    }
                    fcmNotificationMessageText += "Comprador: " + payerEmail + " - (" + description + ")" + " por $ " + transactionAmount + " a las " + dateLastUpdated;

                }
                else if (webhooksDto.Action.Equals("payment.updated"))
                {
                    
                }

            }
            else if (type.Equals("mp-connect"))
            {
                if (webhooksDto.Action.Equals("application.authorized"))
                {

                }
                else if (webhooksDto.Action.Equals("application.deauthorized"))
                {

                }
            }

            var client = new RestClient(ConfigurationManager.AppSettings["FCM_API_BASE_URL"]);

            var fcmRequest = new RestRequest("/fcm/send", Method.POST);
            fcmRequest.RequestFormat = DataFormat.Json;
            fcmRequest.JsonSerializer = new RestSharpJsonNetSerializer();
            fcmRequest.AddHeader("Content-Type", "application/json");
            fcmRequest.AddHeader("Authorization", "key=" + ConfigurationManager.AppSettings["FCM_SERVER_KEY"]);

            var fcmNotificationMessageDto = new FCMNotificationMessageDTO
            {
                Data = new FCMNotificationMessageDTO.FCMNotificationMessageDetailDTO {
                    Type = "notifications",
                    Title = fcmNotificationMessageTitle,
                    Text = fcmNotificationMessageText
                },
                To = registrationToken
            };

            string json = JsonConvert.SerializeObject(fcmNotificationMessageDto, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            fcmRequest.AddJsonBody(fcmNotificationMessageDto);

            IRestResponse fcmResponse = client.Execute(fcmRequest);

            return response;
        }
    }
}

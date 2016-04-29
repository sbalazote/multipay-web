using System;
using System.Collections;
using System.Configuration;
using System.Web.Http;
using mercadopago;
using Multipay.DTOs;
using Newtonsoft.Json;

namespace Multipay.Controllers
{
    public class PaymentController : ApiController
    {
        [HttpGet]
        [Route("api/getPayment")]
        public string GetPayment(string paymentId)
        {
            var mp = new MP(ConfigurationManager.AppSettings["MPAccessToken"]);
            mp.sandboxMode(true);

            var paymentInfo = mp.getPayment(paymentId);

            var response = JsonConvert.SerializeObject(paymentInfo["response"]);

            return response;
        }

        [HttpPost]
        [Route("api/doPayment")]
        public string DoPayment([FromBody] PaymentDataDTO paymentData)
        {
            var mp = new MP(ConfigurationManager.AppSettings["MPAccessToken"]);
            mp.sandboxMode(true);

            var transactionAmount = paymentData.TransactionAmount;
            var cardToken = paymentData.CardToken;
            var paymentMethodId = paymentData.PaymentMethodId;
            var customerId = paymentData.CustomerId;

            var data = "{" +
                "\"transaction_amount\": " + transactionAmount + "," +
                "\"token\": \"" + cardToken + "\"," +
                "\"description\": \"Test Item 01\"," +

                "\"installments\": 1," +
                "\"payment_method_id\": \"" + paymentMethodId + "\"," +
                "\"payer\": {" +
                    "\"id\": \"" + customerId + "\"" +
                    "}" +
                "}";

            var paymentInfo = mp.post("/v1/payments", data);

            var response = JsonConvert.SerializeObject(paymentInfo["response"]);

            return response;
        }
    }
}

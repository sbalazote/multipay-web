using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Http;
using mercadopago;
using Model.Entities;
using Multipay.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services;

namespace Multipay.Controllers
{
    public class PaymentController : ApiController
    {
        private readonly UserService _userService = new UserService();

        [HttpGet]
        [Route("api/getPayment")]
        public string GetPayment(string paymentId)
        {
            var mp = new MP(ConfigurationManager.AppSettings["MPAccessToken"]);

            var paymentInfo = mp.getPayment(paymentId);

            var response = JsonConvert.SerializeObject(paymentInfo["response"]);

            return response;
        }

        [HttpPost]
        [Route("api/doPayment")]
        public JObject DoPayment([FromBody] PaymentDataDTO paymentData)
        {
            paymentData.BuyerEmail = "test_payer_12345789@testuser.com";
            //  Obtengo al Comprador y al Vendedor.
            var buyer = (Buyer)_userService.GetByEmail(paymentData.BuyerEmail, false);
            var seller = (Seller)_userService.GetByEmail(paymentData.SellerEmail, true);

            var mp = new MP(seller.Token.AccessToken);

            // Me fijo si ya existia un Customer con ese email.
            var filters = new Dictionary<String, String> { { "email", paymentData.BuyerEmail } };
            var customerSearch = mp.get("/v1/customers/search", filters);
            var customerSearchResponse = (Hashtable)customerSearch["response"];
            var results = (ArrayList)customerSearchResponse["results"];
            string customerId;
            // No existe el Customer para ese mail.
            if (results.Count == 0)
            {
                var customerSaved = mp.post("/v1/customers", "{\"email\": \"" + paymentData.BuyerEmail + "\"}");
                customerSearchResponse = (Hashtable) customerSaved["response"];
                customerId = (string) customerSearchResponse["id"];
                buyer.MPCustomerId = (string) customerId;
                _userService.Update(buyer);
            }
            // Existe el Customer para ese mail.
            else
            {
                var resultsHashtable = (Hashtable) results[0];
                customerId = (string) resultsHashtable["id"];
            }

            var transactionAmount = paymentData.TransactionAmount;
            var cardToken = paymentData.CardToken;
            var paymentMethodId = paymentData.PaymentMethodId;
            var applicationFee = 0.03f * transactionAmount;
            const string statementDescriptor = "MULTIPAY";

            var data = "{" +
                "\"transaction_amount\": " + transactionAmount + "," +
                "\"token\": \"" + cardToken + "\"," +
                "\"description\": \"Test Item 01\"," +
                "\"installments\": 1," +
                "\"payment_method_id\": \"" + paymentMethodId + "\"," +
                "\"application_fee\": " + applicationFee + "," +
                //"\"statement_descriptor\": " + statementDescriptor + "," +
                //"\"binary_mode\": " + true + "," +
                "\"payer\": {" +
                    "\"id\": \"" + customerId + "\"" +
                    "}" +
                "}";

            var paymentInfo = mp.post("/v1/payments", data);

            // Guardo la tarjeta para que quede asociada si el pago salio de forma correcta.
            var card = mp.post("/v1/customers/" + customerId + "/cards", "{\"token\": \"" + cardToken + "\"}");

            if ((int)paymentInfo["status"] == (int)HttpStatusCode.Created)
            {
                var response = JsonConvert.SerializeObject(paymentInfo["response"]);
                return JObject.Parse(response);
            }
            return new JObject();
        }
    }
}

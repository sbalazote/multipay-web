using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using mercadopago;

namespace Multipay.Controllers
{
    public class PaymentController : ApiController
    {
        [HttpGet]
        [Route("api/getPayment")]
        public string GetPayment(string paymentId)
        {
            MP mp = new MP(ConfigurationManager.AppSettings["MPAccessToken"]);

            Hashtable paymentInfo = mp.getPayment(paymentId);

            Console.Write(paymentInfo["response"]);

            return paymentInfo["response"].ToString();
        }

        [HttpPost]
        [Route("api/doPayment")]
        public string DoPayment(string paymentData)
        {
            MP mp = new MP(ConfigurationManager.AppSettings["MPAccessToken"]);

            Hashtable paymentInfo = mp.post("/v1/payments", paymentData);

            Console.Write(paymentInfo["response"]);

            return paymentInfo["response"].ToString();
        }
    }
}

using System;
using System.Configuration;
using System.Web.Http;
using System.Collections;
using mercadopago;

namespace Multipay.Controllers
{
    public class CustomerController : ApiController
    {
        [HttpGet]
        [Route("api/getCustomerCards")]
        public string GetCustomerCards(string customerId)
        {
            MP mp = new MP(ConfigurationManager.AppSettings["MPAccessToken"]);

            Hashtable cards = mp.get ("/v1/customers/"+customerId+"/cards", true);

            Console.WriteLine (cards["response"].ToString());

            return cards["response"].ToString();
        }

        [HttpGet]
        [Route("api/getCustomer")]
        public string GetCustomer(string customerId)
        {
            MP mp = new MP(ConfigurationManager.AppSettings["MPAccessToken"]);

            Hashtable customer = mp.get("/v1/customers/"+customerId, true);

            Console.WriteLine(customer["response"].ToString());

            return customer["response"].ToString();
        }
    }
}
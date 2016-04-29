using System.Configuration;
using System.Web.Http;
using mercadopago;
using Newtonsoft.Json;

namespace Multipay.Controllers
{
    public class CustomerController : ApiController
    {
        [HttpGet]
        [Route("api/getCustomerCards")]
        public string GetCustomerCards(string customerId)
        {
            var mp = new MP(ConfigurationManager.AppSettings["MPAccessToken"]);
            mp.sandboxMode(true);

            var cards = mp.get ("/v1/customers/"+customerId+"/cards", true);

            var response = JsonConvert.SerializeObject(cards["response"]);

            return response;
        }

        [HttpGet]
        [Route("api/getCustomer")]
        public string GetCustomer(string customerId)
        {
            var mp = new MP(ConfigurationManager.AppSettings["MPAccessToken"]);
            mp.sandboxMode(true);

            var customer = mp.get("/v1/customers/"+customerId, true);

            var response = JsonConvert.SerializeObject(customer["response"]);

            return response;
        }

        [HttpPost]
        [Route("api/customers/{customerId}/cards")]
        public string AddNewCardToCustomer(string customerId, [FromBody] string cardToken)
        {
            var mp = new MP(ConfigurationManager.AppSettings["MPAccessToken"]);
            mp.sandboxMode(true);

            var card = mp.post("/v1/customers/" + customerId + "/cards", "{\"token\": \""+cardToken+"\"}");

            var response = JsonConvert.SerializeObject(card["response"]);

            return response;
        }
    }
}
using System.Configuration;
using System.Web.Http;
using mercadopago;
using Model.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services;

namespace Multipay.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly UserService _userService = new UserService();

        [HttpGet]
        [Route("api/getCustomer")]
        public JObject GetCustomer([FromUri(Name = "seller_email")] string sellerEmail, [FromUri(Name = "buyer_email")] string buyerEmail)
        {
            var buyer = (Buyer)_userService.GetByEmail(buyerEmail, false);
            var seller = (Seller)_userService.GetByEmail(sellerEmail, true);

            var mp = new MP(seller.Token.AccessToken);

            var customer = mp.get("/v1/customers/"+buyer.MPCustomerId, true);

            if (customer["response"] != null)
            {
                var response = JsonConvert.SerializeObject(customer["response"]);
                return JObject.Parse(response);
            }
            return new JObject();
        }
    }
}
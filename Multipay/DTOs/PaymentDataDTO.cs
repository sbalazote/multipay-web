using Newtonsoft.Json;

namespace Multipay.DTOs
{
    public class PaymentDataDTO
    {
        [JsonProperty("card_token")]
        public string CardToken { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("transaction_amount")]
        public float TransactionAmount { get; set; }
        [JsonProperty("payment_method_id")]
        public string PaymentMethodId { get; set; }
        [JsonProperty("buyer_email")]
        public string BuyerEmail { get; set; }
        [JsonProperty("seller_email")]
        public string SellerEmail { get; set; }
    }
}
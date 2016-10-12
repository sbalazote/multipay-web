using Newtonsoft.Json;

namespace Multipay.DTOs
{
    public class FCMNotificationMessageDTO
    {
        [JsonProperty("data")]
        public FCMNotificationMessageDetailDTO Data { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }

        public class FCMNotificationMessageDetailDTO
        {
            [JsonProperty("type")]
            public string Type { get; set; }
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("text")]
            public string Text { get; set; }
            [JsonProperty("description")]
            public string Description { get; set; }
            [JsonProperty("seller_email")]
            public string SellerEmail { get; set; }
            [JsonProperty("transaction_amount")]
            public string TransactionAmount { get; set; }
        }
    }
}
using Newtonsoft.Json;

namespace Multipay.DTOs
{
    public class FCMNotificationMessageDTO
    {
        [JsonProperty("notification")]
        public FCMNotificationMessageDetailDTO Notification { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }

        public class FCMNotificationMessageDetailDTO
        {
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("text")]
            public string Text { get; set; }
        }
    }
}
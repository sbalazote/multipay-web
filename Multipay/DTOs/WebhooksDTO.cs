/*
 * {
 * "id":123,
 * "live_mode":false,
 * "type":"test",
 * "date_created":"2016-05-02T17:12:39.787-04:00",
 * "user_id":"73449193",
 * "api_version":"v1",
 * "action":"test.created",
 * "data":{"id":"56456123212"}}
 */
using Newtonsoft.Json;

namespace Multipay.DTOs
{
    public class WebhooksDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("live_mode")]
        public bool LiveMode { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("date_created")]
        public string DateCreated { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("api_version")]
        public string ApiVersion { get; set; }
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("data")]
        public WebhooksDataDTO Data { get; set; }
    }

    public class WebhooksDataDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
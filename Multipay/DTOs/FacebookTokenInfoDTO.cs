using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
 * "id":"10206145954277821"
 * "email":"sbalazote@gmail.com"
 * "first_name":"Sebastian",
 * "gender":"male"
 * "last_name":"Balazote"
 * "link":"https://www.facebook.com//app_scoped_user_id/10206145954277821/
 * "locale":"es_LA",
 * "name":"Sebastian Balazote"
 * "timezone":-3
 * "updated_time":"2016-04-06T15:33:51+0000"
 * "verified":true"
 * */

namespace Multipay.DTOs
{
    public class FacebookTokenInfoDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("locale")]
        public string Locale { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("timezone")]
        public string Timezone { get; set; }
        [JsonProperty("updated_time")]
        public string UpdatedTime { get; set; }
        [JsonProperty("verified")]
        public Boolean Verified { get; set; }
    }
}
﻿using Newtonsoft.Json;

namespace Multipay.DTOs
{
    public class AuthorizationTokenDTO
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
       
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        
        [JsonProperty("scope")]
        public string Scope { get; set; }
        
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
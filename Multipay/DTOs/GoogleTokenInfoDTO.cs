using Newtonsoft.Json;

/*
 * {
 // These six fields are included in all Google ID Tokens.
 "iss": "https://accounts.google.com",
 "sub": "110169484474386276334",
 "azp": "1008719970978-hb24n2dstb40o45d4feuo2ukqmcc6381.apps.googleusercontent.com",
 "aud": "1008719970978-hb24n2dstb40o45d4feuo2ukqmcc6381.apps.googleusercontent.com",
 "iat": "1433978353",
 "exp": "1433981953",

 // These seven fields are only included when the user has granted the "profile" and
 // "email" OAuth scopes to the application.
 "email": "testuser@gmail.com",
 "email_verified": "true",
 "name" : "Test User",
 "picture": "https://lh4.googleusercontent.com/-kYgzyAWpZzJ/ABCDEFGHI/AAAJKLMNOP/tIXL9Ir44LE/s99-c/photo.jpg",
 "given_name": "Test",
 "family_name": "User",
 "locale": "en"
}
 *
 * Once you get these claims, you still need to check that the aud claim contains one of your app's client IDs. 
 * If it does, then the token is both valid and intended for your client, and you can safely retrieve and use the user's unique Google ID from the sub claim.
 */

namespace Multipay.DTOs
{
    public class GoogleTokenInfoDTO
    {
        public const string GoogleOauthServerClientId = "437797444824-6ovnf5l1l1he589sv32tm21qciamiqml.apps.googleusercontent.com";

        [JsonProperty("iss")]
        public string Iss { get; set; }
        [JsonProperty("sub")]
        public string Sub { get; set; }
        [JsonProperty("azp")]
        public string Azp { get; set; }
        [JsonProperty("aud")]
        public string Aud { get; set; }
        [JsonProperty("iat")]
        public string Iat { get; set; }
        [JsonProperty("exp")]
        public string Exp { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("email_verified")]
        public string EmailVerified { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("picture")]
        public string Picture { get; set; }
        [JsonProperty("given_name")]
        public string GivenName { get; set; }
        [JsonProperty("family_name")]
        public string FamilyName { get; set; }
        [JsonProperty("locale")]
        public string Locale { get; set; }
    }
}
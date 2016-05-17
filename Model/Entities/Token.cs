using System;

namespace Model.Entities
{
    public class Token
    {
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public string Type { get; set; }
        public string RefreshToken { get; set; }
        public string PublicKey { get; set; }
        public int Expiration { get; set; }
        public string Scope { get; set; }
        public DateTime RequestedTime { get; set; }
    }
}
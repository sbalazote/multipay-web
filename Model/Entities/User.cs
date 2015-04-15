using System;

namespace Model.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string AuthCode { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int TokenExpires { get; set; }
        public DateTime TokenRequested { get; set; }
        public string UserMLA { get; set; }
        public bool Active { get; set; }
    }
}
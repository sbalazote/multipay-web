using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multipay.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        public string Password { get; set; }
        public string AuthCode { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int TokenExpires { get; set; }
        public DateTime TokenRequested { get; set; }
        public string UserMLA { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
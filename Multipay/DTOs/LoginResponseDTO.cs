using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multipay.DTO
{
    public class LoginResponseDTO
    {
        public bool Valid { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
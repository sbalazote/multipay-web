using System.ComponentModel.DataAnnotations;

namespace Multipay.DTOs
{
    public class LoginRequestDTO
    {
        [Required]
        public string MobileId { get; set; }
        [Required]
        public string RegistrationId { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string SocialToken { get; set; }
        [Required]
        public bool IsSeller { get; set; }
    }
}
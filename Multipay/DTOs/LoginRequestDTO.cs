using System.ComponentModel.DataAnnotations;

namespace Multipay.DTO
{
    public class BuyerRegisterDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string IndentificationType { get; set; }
        public string IntificationNumber { get; set; }
        public string RegistrationId { get; set; }
        public string AddressName { get; set; }
        public int AddessNumber { get; set; }
        public string ZipCode { get; set; }
        public int PhoneAreaCode { get; set; }
        public string  PhoneNumber { get; set; }

    }

    public class LoginRequestDTO
    {
        [Required]
        public int MobileId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string SocialToken { get; set; }
        [Required]
        public bool IsSeller { get; set; }
    }
}
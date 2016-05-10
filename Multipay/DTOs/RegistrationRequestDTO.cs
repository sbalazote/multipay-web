using System.ComponentModel.DataAnnotations;

namespace Multipay.DTOs
{
    public class RegistrationRequestDTO
    {
        [Required]
        public string MobileId { get; set; }
        [Required]
        public string RegistrationId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public string AddressName { get; set; }
        public int AddressNumber { get; set; }
        public string AddressZipCode { get; set; }
        public int PhoneAreaCode { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public bool IsSeller { get; set; }
    }
}
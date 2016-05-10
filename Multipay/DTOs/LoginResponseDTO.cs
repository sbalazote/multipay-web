namespace Multipay.DTOs
{
    public class LoginResponseDTO
    {
        public bool Valid { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
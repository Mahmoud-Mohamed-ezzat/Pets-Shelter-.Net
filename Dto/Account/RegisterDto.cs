using System.ComponentModel.DataAnnotations;

namespace Animal2.Dto.Account
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}

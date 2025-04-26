using System.ComponentModel.DataAnnotations;

namespace Animal2.Dto.Account
{
    public class UpdateShelterstaff
    {
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ShelterAddress { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Animal2.Dto.Account
{
    public class ShelterstaffRetrieve
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ShelterAddress { get; set; }
    }
}

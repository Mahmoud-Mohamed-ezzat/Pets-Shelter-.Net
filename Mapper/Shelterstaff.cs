using Animal2.Dto.Account;
using Animal2.Models;

namespace Animal2.Mapper
{
    public static class Shelterstaff
    {
        public static ShelterstaffRetrieve ToshelterstaffRetrieve(this Customer shelterstaff)
        {
            return new ShelterstaffRetrieve
            {
                Id = shelterstaff.Id,
                UserName = shelterstaff.UserName,
                PhoneNumber = shelterstaff.PhoneNumber,
                Email=shelterstaff.Email,
                ShelterAddress=shelterstaff.ShelterAddress
            };
        }  public static ShelterstaffRetrieve ToshelterstaffRetrieve2(this Customer shelterstaff)
        {
            return new ShelterstaffRetrieve
            {
                UserName = shelterstaff.UserName,
                PhoneNumber = shelterstaff.PhoneNumber,
                Email=shelterstaff.Email,
                ShelterAddress=shelterstaff.ShelterAddress
            };
        }
    }
}

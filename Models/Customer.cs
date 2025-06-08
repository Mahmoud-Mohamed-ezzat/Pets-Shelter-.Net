using Animal2.Mapper;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Animal2.Models
{
    [Table("Customer")]
    public class Customer:IdentityUser
    {
        [ForeignKey("UserCategoryId")]
       public virtual UserCategory UserCategory { get; set; } 
       public int UserCategoryId { get; set; }
        public string ? ShelterAddress { get; set; }
        public virtual ICollection<Animal> AnimalsAsShelterStaff { get; set; } = new List<Animal>();
        public virtual ICollection<Animal> AnimalsAsAdopter { get; set; } = new List<Animal>();
        public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
        public virtual ICollection<Message> SenderMessages { get; set; } = new List<Message>();
        public virtual ICollection<Message> ReceiverMessages { get; set; } = new List<Message>();

        //public virtual ICollection<Shelter> Shelter{ get; set; }



    }
}

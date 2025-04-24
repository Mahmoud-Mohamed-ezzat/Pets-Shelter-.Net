using Microsoft.AspNetCore.JsonPatch.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Animal2.Models
{
    [Table("Breed")]
    public class Breed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        //[JsonIgnore]
        [ForeignKey("AnimalCategoryId")]
        public  virtual AnimalCategory AnimalCategory { get; set; }
       public int AnimalCategoryId { get; set; }
        public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();


    }
}

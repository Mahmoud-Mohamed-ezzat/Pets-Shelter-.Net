using Animal2.Dto.animal;
using Animal2.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Animal2.Dto.Breed
{
    public class BreedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AnimalCategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}

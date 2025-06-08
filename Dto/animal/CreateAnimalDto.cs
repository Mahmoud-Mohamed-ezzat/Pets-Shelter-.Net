using System.ComponentModel.DataAnnotations;

namespace Animal2.Dto.animal
{
    public class CreateAnimalDto
    {
        public string AnimalName { get; set; }
        public string AnimalGender { get; set; }
        public string AnimalStatus { get; set; }
        public string AnimalHistory { get; set; }
        public IFormFile Image { get; set; }
        public string Age { get; set; }
        public int AnimalCategoryId { get; set; }
        public string ShelterStaffId { get; set; }
        public int BreedId { get; set; }
    }
}

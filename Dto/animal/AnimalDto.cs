using Animal2.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Animal2.Dto.animal
{
    public class AnimalDto
    {
        public int Id { get; set; }
        public string AnimalName { get; set; }
        public string AnimalGender { get; set; }
        public string AnimalStatus { get; set; }
        public string AnimalHistory { get; set; }
        public string Image { get; set; }
        public string Age { get; set; }
        public string CategoryName { get; set; }
        public string BreedName { get; set; }
        public string ShelterName { get; set; }
        public string? AdopterName { get; set; }
    }
}


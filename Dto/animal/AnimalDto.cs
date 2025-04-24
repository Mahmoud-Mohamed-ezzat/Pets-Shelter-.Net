namespace Animal2.Dto.animal
{
    public class AnimalDto
    {
        public int Id { get; set; }

        public string? AnimalName { get; set; }

        public string? AnimalGender { get; set; }

        public string? AnimalStatus { get; set; }

        public string? AnimalHistory { get; set; }

        public string? AnimalBreed { get; set; }
        public IFormFile Image { get; set; }
        public string? Age { get; set; }

        public int? CategoryAnimalId { get; set; }

        public string ShelterStaffId { get; set; }

        public string AdopterId { get; set; }
    }
}

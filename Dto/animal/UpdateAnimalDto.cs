namespace Animal2.Dto.animal
{
    public class UpdateAnimalDto
    {
        public string AnimalName { get; set; }
        public string AnimalStatus { get; set; }
        public string AnimalHistory { get; set; }
        //public string ImageN { get; set; }
        public IFormFile Image { get; set; }
        public string Age { get; set; }
    }
}

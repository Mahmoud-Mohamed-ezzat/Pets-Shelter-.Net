using Animal2.Dto.animal;
using Animal2.Dto.Breed;

namespace Animal2.Dto.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string? CategoryName { get; set; }
        public List<AnimalDto> animals { get; set; }
        public List<BreedDto> Breeds { get; set; }
    }
}

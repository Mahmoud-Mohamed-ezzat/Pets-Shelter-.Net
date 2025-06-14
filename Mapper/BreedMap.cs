using Animal2.Dto.Breed;
using Animal2.Models;

namespace Animal2.Mapper
{
    public static class BreedMap
    {
        public static BreedDto ToBreedDto(this Breed BreedModel)
        {
            return new BreedDto
            {
                Id = BreedModel.Id, 
                Name = BreedModel.Name,
                AnimalCategoryId = BreedModel.AnimalCategoryId,
                CategoryName=BreedModel.AnimalCategory.CategoryName
            };
        }
        public static Breed CreateBreedDto(this CreateBreedDto breedDto) {
            return new Breed
            {
                Name = breedDto.Name,
                AnimalCategoryId = breedDto.AnimalCategoryId,
            };
        
        }
    }
}

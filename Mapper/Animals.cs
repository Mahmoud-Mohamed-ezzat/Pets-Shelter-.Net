using Animal2.Dto.animal;
using Animal2.Models;

namespace Animal2.Mapper
{
    public static class Animals
    {
        public static AnimalDto ToAnimalDto(this Animal animal)
        {
            return new AnimalDto
            {
                Id = animal.Id,
                AnimalName = animal.AnimalName,
                AnimalGender = animal.AnimalGender,
                AnimalStatus = animal.AnimalStatus,
                AnimalHistory = animal.AnimalHistory,
                Age = animal.Age,
                Image = animal.Image,
                CategoryName = animal.AnimalCategory.CategoryName,
                AdopterName = animal.Adopter?.UserName,
                ShelterName = animal.ShelterStaff?.ShelterAddress,
                BreedName = animal.Breed?.Name,
            };
        }
        public static ShelterAnimalsDto ToShelterAnimalDto(this Animal animal)
        {
            return new ShelterAnimalsDto
            {
                Id = animal.Id,
                AnimalName = animal.AnimalName,
                AnimalGender = animal.AnimalGender,
                AnimalHistory = animal.AnimalHistory,
                AnimalStatus = animal.AnimalStatus,
                Image = animal.Image,
                Age = animal.Age,
                CategoryName = animal.AnimalCategory.CategoryName,
                BreedName = animal.Breed.Name,
                AdopterName = animal.Adopter?.UserName,
            };
        }
        public static Animal CreateAnimalDtoToAnimal(this CreateAnimalDto createAnimalDto)
        {
            return new Animal
            {
                AnimalName = createAnimalDto.AnimalName,
                AnimalGender = createAnimalDto.AnimalGender,
                AnimalStatus = createAnimalDto.AnimalStatus,
                AnimalHistory = createAnimalDto.AnimalHistory,
                Age = createAnimalDto.Age,
                AnimalCategoryId = createAnimalDto.AnimalCategoryId,
                ShelterStaffId = createAnimalDto.ShelterStaffId,
                BreedId = createAnimalDto.BreedId,
            };
        }
    }
}

using Animal2.Dto.animal;
using Animal2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Animal2.Interface
{
    public interface IAnimal
    {
        Task<List<ShelterAnimalsDto>> ShelterAnimals(string id);
        Task<List<AnimalDto>> AllAnimals();
        Task<AnimalDto> GetOneAnimal(int id);
        Task<List<AnimalDto>> GetByCategory(string category);
        Task<List<AnimalDto>> GetByBreed(string breed);
        Task<Animal> AddAnimal(CreateAnimalDto createAnimalDto);
        Task<Animal> UpdateAnimal(int id, UpdateAnimalDto updateAnimalDto);
        Task<Animal> DeleteAnimal(int id);
    }
}

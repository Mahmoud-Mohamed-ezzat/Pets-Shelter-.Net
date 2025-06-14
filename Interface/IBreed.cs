using Animal2.Dto.Breed;
using Animal2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Animal2.Interface
{
    public interface IBreed
    {
        Task<List<BreedDto>> GetAll();
        Task<List<BreedDto>> GetBySearch(string search);
        Task<BreedDto> GetByID(int id);
        Task<Breed> CreateBreed(CreateBreedDto breeddto);
        Task<Breed> DeleteBreed(int id);
        Task<Breed> UpdateBreed(int id, CreateBreedDto createBreedDto);

    }
}

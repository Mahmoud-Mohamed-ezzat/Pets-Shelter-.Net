using Animal2.Dto.Breed;
using Animal2.Interface;
using Animal2.Mapper;
using Animal2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Animal2.Repository
{
    public class BreedRepository : IBreed
    {
        private readonly AnimalsContext _context;
        public BreedRepository(AnimalsContext context)
        {
            _context = context;
        }
        public async Task<Breed> CreateBreed(CreateBreedDto breeddto)
        {
            var BreedModel = breeddto.CreateBreedDto();
            await _context.AddAsync(BreedModel);
            await _context.SaveChangesAsync();
            return BreedModel;
        }
        public async Task<Breed> DeleteBreed(int id)
        {
            var BreedModel = await _context.Breeds.FirstOrDefaultAsync(b => b.Id == id);
            if (BreedModel == null) { return null; }
            _context.Remove(BreedModel);
            await _context.SaveChangesAsync();
            return BreedModel;
        }

        public async Task<List<BreedDto>> GetAll()
        {
            var breeds = await _context.Breeds.Include(b => b.AnimalCategory).Select(b => b.ToBreedDto()).ToListAsync();
            return breeds;
        }

        public async Task<BreedDto> GetByID(int id)
        {
            var breed = await _context.Breeds.Include(b => b.AnimalCategory).FirstOrDefaultAsync(b => b.Id == id);
            var BreedDto = breed.ToBreedDto();
            return BreedDto;
        }

        public async Task<List<BreedDto>> GetBySearch(string search)
        {
            var breeds = await _context.Breeds.Include(b => b.AnimalCategory).Where(b => b.Name.Contains(search)).ToListAsync();
            var BreedDto = breeds.Select(b => b.ToBreedDto()).ToList();
            return BreedDto;
        }

        public async Task<Breed> UpdateBreed(int id, CreateBreedDto createBreedDto)
        {
            var breedmodel = await _context.Breeds.FirstOrDefaultAsync(b => b.Id == id);
            if (breedmodel == null) { return null; }

            breedmodel.Name = createBreedDto.Name;
            breedmodel.AnimalCategoryId = createBreedDto.AnimalCategoryId;
            await _context.SaveChangesAsync();
            return breedmodel;
        }
    }
}

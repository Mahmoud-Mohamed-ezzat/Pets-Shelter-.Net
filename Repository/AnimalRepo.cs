using Animal2.Dto.animal;
using Animal2.Interface;
using Animal2.Mapper;
using Animal2.Models;
using Microsoft.EntityFrameworkCore;

namespace Animal2.Repository
{
    public class AnimalRepo : IAnimal
    {
        private readonly AnimalsContext _context;
        public AnimalRepo(AnimalsContext context)
        {
            _context = context;
        }

        public async Task<Animal> AddAnimal(CreateAnimalDto createAnimalDto)
        {
            string FilePath = Path.Combine(@"E:\IA\Animal2\Images\", createAnimalDto.AnimalName + ".Png");
            using (Stream stream = new FileStream(FilePath, FileMode.Create))
            {
                createAnimalDto.Image.CopyTo(stream);
            }
            var animal = createAnimalDto.CreateAnimalDtoToAnimal();
            animal.Image = FilePath;
            await _context.Animals.AddAsync(animal);
            await _context.SaveChangesAsync();
            return animal;
        }

        public async Task<List<AnimalDto>> AllAnimals()
        {
            var animals = await _context.Animals.Include(a => a.ShelterStaff).Include(a => a.Breed).Include(a => a.Adopter).Include(a => a.AnimalCategory).ToListAsync();
            var animalDtos = animals.Select(a => a.ToAnimalDto()).ToList();
            return animalDtos;
        }

        public async Task<Animal> DeleteAnimal(int id)
        {
            var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);
            if (animal == null)
            {
                return null;
            }
            string filePath = Path.Combine(@"E:\IA\Animal2\Images\", animal.Image);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            _context.Remove(animal);
            await _context.SaveChangesAsync();
            return animal;
        }

        public async Task<List<AnimalDto>> GetByBreed(string breed)
        {
            var animals = await _context.Animals.Include(a => a.Breed).Include(a => a.AnimalCategory).Include(a => a.ShelterStaff).ToListAsync();
            var animalsbyBreed = animals.Where(b => b.Breed != null && b.Breed.Name == breed).ToList();
            if (animalsbyBreed == null)
            {
                return null;
            }
            var animalDtos = animalsbyBreed.Select(a => a.ToAnimalDto()).ToList();
            return animalDtos;
        }

        public async Task<List<AnimalDto>> GetByCategory(string category)
        {
            var animals = await _context.Animals.Include(a => a.ShelterStaff).Include(a => a.AnimalCategory).ToListAsync();
            var animalsbycategory = animals.Where(b => b.AnimalCategory != null && b.AnimalCategory.CategoryName == category).ToList();
            if (animalsbycategory == null)
            {
                return null;
            }
            var animalDtos = animalsbycategory.Select(a => a.ToAnimalDto()).ToList();
            return animalDtos;
        }

        public async Task<AnimalDto> GetOneAnimal(int id)
        {
            var animalDto = await _context.Animals.Include(a => a.AnimalCategory).Include(a => a.ShelterStaff).Include(c => c.Breed).Include(b => b.Adopter).Where(i => i.Id == id).Select(a => a.ToAnimalDto()).FirstOrDefaultAsync();

            if (animalDto == null)
            {
                return null;
            }
            return animalDto;
        }

        public async Task<List<ShelterAnimalsDto>> ShelterAnimals(string id)
        {
            var animals = await _context.Animals.Include(a => a.Breed).Include(a => a.Adopter).Include(a => a.AnimalCategory).Where(b => b.ShelterStaffId == id).ToListAsync();
            var animalDtos = animals.Select(a => a.ToShelterAnimalDto()).ToList();
            return animalDtos;
        }

        public async Task<Animal> UpdateAnimal(int id, UpdateAnimalDto updateAnimalDto)
        {
            var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);
            if (animal == null)
            {
                return null;
            }
            string filePath = Path.Combine(@"E:\IA\Animal2\Images\", animal.Image);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            animal.AnimalName = updateAnimalDto.AnimalName;
            animal.AnimalStatus = updateAnimalDto.AnimalStatus;
            animal.AnimalHistory = updateAnimalDto.AnimalHistory;
            animal.Age = updateAnimalDto.Age;
            string FilePath = Path.Combine(@"E:\IA\Animal2\Images\", updateAnimalDto.AnimalName + ".PNG");
            using (Stream stream = new FileStream(FilePath, FileMode.Create))
            {
                updateAnimalDto.Image.CopyTo(stream);
            }
            animal.Image = FilePath;
            await _context.SaveChangesAsync();
            return animal;
        }
    }
}

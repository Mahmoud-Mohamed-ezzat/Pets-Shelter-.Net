using Animal2.Dto.animal;
using Animal2.Mapper;
using Animal2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Animal2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly AnimalsContext _context;
        public AnimalController(AnimalsContext _context)
        {
            this._context = _context;
        }

        [Authorize (Roles ="Shelterstaff")]
        [HttpGet("Shelter/{id}")]
        public async Task<IActionResult> ShelterAnimals(string id) //authorized by shelter staff
        {
            var animals = await _context.Animals.Include(a => a.Breed).Include(a => a.Adopter).Include(a => a.AnimalCategory).Where(b => b.ShelterStaffId == id).ToListAsync();
            var animalDtos = animals.Select(a=>a.ToShelterAnimalDto()).ToList();  
            return Ok(animalDtos);
        }

        [Authorize]
        [HttpGet("Adopter")]
        public async Task<IActionResult> AllAnimals() 
        {
            var animals = await _context.Animals.Include(a => a.ShelterStaff).Include(a => a.Breed).Include(a => a.Adopter).Include(a => a.AnimalCategory).ToListAsync();
            var animalDtos = animals.Select(a => a.ToAnimalDto()).ToList();
            return Ok(animalDtos);
        }


        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneAnimal( int id) //autherized by adopter
        {
            var animal = await _context.Animals.Include(a=>a.AnimalCategory).Include(a => a.ShelterStaff).Include(c => c.Breed).Include(b => b.Adopter)

                .Where(i => i.Id == id).Select(a =>a.ToAnimalDto()).FirstOrDefaultAsync();

            if (animal == null)
            {
                return NotFound();
            }
            return Ok(animal);
        }

        [Authorize]
        [HttpGet("GetByCategory/{category}")]
        public async Task<IActionResult> GetByCategory(string category) //autherized by adopter (search)
        {
            var animals = await _context.Animals.Include(a=>a.ShelterStaff).Include(a => a.AnimalCategory).ToListAsync();
            var animalsbycategory = animals.Where(b => b.AnimalCategory != null && b.AnimalCategory.CategoryName== category).ToList();
            if (animalsbycategory == null)
            {
                return NotFound();
            }
            var animalDtos = animalsbycategory.Select(a=>a.ToAnimalDto()).ToList(); 
            return Ok(animalDtos);
        }

        [Authorize]
        [HttpGet("GetByBreed/{breed}")] //autherized by adopter (filter)
        public async Task<IActionResult> GetByBreed(string breed)
        {
            var animals = await _context.Animals.Include(a => a.Breed).Include(a=>a.AnimalCategory).Include(a=>a.ShelterStaff).ToListAsync();
            var animalsbycategory = animals.Where(b => b.Breed != null && b.Breed.Name == breed).ToList();
            if (animalsbycategory == null)
            {
                return NotFound();
            }
            var animalDtos = animalsbycategory.Select(a => a.ToAnimalDto()).ToList(); 
            return Ok(animalDtos);
        }

        [Authorize(Roles = "Shelterstaff")]
        [HttpPost]
        public async Task<IActionResult> AddAnimal([FromForm] CreateAnimalDto createAnimalDto ) //autherized by shelter staff
        {
            string FilePath = Path.Combine(@"E:\IA\Animal2\Images\", createAnimalDto.AnimalName+".Png");
            using (Stream stream = new FileStream(FilePath, FileMode.Create ))
            {
                createAnimalDto.Image.CopyTo(stream);
            }
            var animal = createAnimalDto.CreateAnimalDtoToAnimal();
            animal.Image = FilePath;
            await _context.Animals.AddAsync(animal);
            await _context.SaveChangesAsync();
            return Ok(animal);
        }

        [Authorize(Roles = "Shelterstaff")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnimal(int id, [FromForm] UpdateAnimalDto updateAnimalDto) //autherized by shelter staff
        {
            var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);
            if (animal == null)
            {
                return NotFound();
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
            return Ok(animal);
        }

        [Authorize(Roles = "Shelterstaff")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id) //autherized by shelterStaff
        {
            var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);
            if (animal == null)
            {
                return NotFound();
            }
            string filePath = Path.Combine(@"E:\IA\Animal2\Images\", animal.Image);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            _context.Remove(animal);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //public  async Task<string> SaveImageFile(IFormFile imageFile)
        //{
        //    // Validate file type
        //    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        //    var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
        //    if (!allowedExtensions.Contains(fileExtension))
        //    {
        //        throw new ArgumentException("Invalid file type. Only JPG, PNG, and GIF are allowed.");
        //    }

        //    // Get storage path from configuration
        //    var storagePath = _configuration["FileStorage:Path"] ?? "FileStorage";
        //    var fullStoragePath = Path.Combine(Directory.GetCurrentDirectory(), storagePath, "Animals");

        //    // Create directory if it doesn't exist
        //    if (!Directory.Exists(fullStoragePath))
        //    {
        //        Directory.CreateDirectory(fullStoragePath);
        //    }

        //    // Generate unique filename
        //    var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
        //    var filePath = Path.Combine(fullStoragePath, uniqueFileName);

        //    // Save the file
        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await imageFile.CopyToAsync(fileStream);
        //    }

        //    // Return relative path (storage/specifications/filename.ext)
        //    return Path.Combine("storage", "Animals", uniqueFileName).Replace("\\", "/");
        //}
        //private void DeleteImageFile(string imagePath)
        //{
        //    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), imagePath.Replace("/", "\\"));
        //    if (System.IO.File.Exists(fullPath))
        //    {
        //        System.IO.File.Delete(fullPath);
        //    }
        //}
        //private string GetFullImageUrl(string relativePath)
        //{
        //    if (string.IsNullOrEmpty(relativePath)) return null;

        //    var baseUrl = _configuration["ApiBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";
        //    return $"{baseUrl}/api/file/{relativePath}";
        //}
    }
}
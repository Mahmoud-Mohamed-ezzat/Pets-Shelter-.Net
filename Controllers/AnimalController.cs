using Animal2.Dto.animal;
using Animal2.Mapper;
using Animal2.Models;
using Animal2.Repository;
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
        private readonly AnimalRepo _animalRepo;
        public AnimalController(AnimalsContext _context, AnimalRepo animalRepo)
        {
            this._context = _context;
            this._animalRepo = animalRepo;
        }

        [Authorize (Roles ="Shelterstaff")]
        [HttpGet("Shelter/{id}")]
        public async Task<IActionResult> ShelterAnimals(string id) //authorized by shelter staff
        {
            var animalDtos = await _animalRepo.ShelterAnimals(id);
            return Ok(animalDtos);
        }

        [Authorize]
        [HttpGet("Adopter")]
        public async Task<IActionResult> AllAnimals() 
        {
            var animalDtos = await _animalRepo.AllAnimals();
            return Ok(animalDtos);
        }


        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneAnimal( int id) //autherized by adopter
        {
            var animal = await _animalRepo.GetOneAnimal(id);
            if (animal == null) return NotFound();
            return Ok(animal);
        }

        [Authorize]
        [HttpGet("GetByCategory/{category}")]
        public async Task<IActionResult> GetByCategory(string category) //autherized by adopter (search)
        {
            var animalDtos = await _animalRepo.GetByCategory(category);
            if (animalDtos == null) return NotFound();
            return Ok(animalDtos);
        }

        [Authorize]
        [HttpGet("GetByBreed/{breed}")] //autherized by adopter (filter)
        public async Task<IActionResult> GetByBreed(string breed)
        {
            var animalDtos = await _animalRepo.GetByBreed(breed);
            if (animalDtos == null) return NotFound();
            return Ok(animalDtos);
        }

        [Authorize(Roles = "Shelterstaff")]
        [HttpPost]
        public async Task<IActionResult> AddAnimal([FromForm] CreateAnimalDto createAnimalDto ) //autherized by shelter staff
        {
            var animal =await _animalRepo.AddAnimal(createAnimalDto);
            return Ok(animal);
        }

        [Authorize(Roles = "Shelterstaff")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnimal(int id, [FromForm] UpdateAnimalDto updateAnimalDto) //autherized by shelter staff
        {
            var animal = await _animalRepo.UpdateAnimal(id, updateAnimalDto);
            if (animal == null) return NotFound();
            return Ok(animal);
        }

        [Authorize(Roles = "Shelterstaff")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id) //autherized by shelterStaff
        {
            var animal = await _animalRepo.DeleteAnimal(id);
            if (animal == null) return NotFound();
            return Ok(animal);
        }
    }
}
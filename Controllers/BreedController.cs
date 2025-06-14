using Animal2.Dto.Breed;
using Animal2.Interface;
using Animal2.Mapper;
using Animal2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Animal2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedController : ControllerBase
    {
        private readonly AnimalsContext _context;
        private readonly IBreed _BreedRepo;
        public BreedController(AnimalsContext context, IBreed BreedRepo)
        {
            _context = context;
            _BreedRepo = BreedRepo;
        }

        [Authorize]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var breeds = await _BreedRepo.GetAll();
            return Ok(breeds);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get(string search)
        {
            var breeds = await _BreedRepo.GetBySearch(search);
            return Ok(breeds);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var breed = await _BreedRepo.GetByID(id);
            return Ok(breed);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateBreed([FromBody] CreateBreedDto breeddto)
        {
            var BreedModel = await _BreedRepo.CreateBreed(breeddto);
            return CreatedAtAction(nameof(GetById), new { id = BreedModel.Id }, BreedModel.Name);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteBreed(int id)
        {
            var BreedModel = await _BreedRepo.DeleteBreed(id);
            return Ok(BreedModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateBreed(int id, [FromForm] CreateBreedDto createBreedDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var breedmodel = await _BreedRepo.UpdateBreed(id, createBreedDto);
            return Ok(breedmodel.ToBreedDto());
        }
    }
}
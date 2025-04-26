using Animal2.Dto.Breed;
using Animal2.Mapper;
using Animal2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Animal2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedController : ControllerBase
    {
        private readonly AnimalsContext _context;
        public BreedController(AnimalsContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var breeds = await _context.Breeds.Include(b => b.AnimalCategory).Select(b => new { Id=b.Id,Name=b.Name, CategoryName=b.AnimalCategory.CategoryName ,CategoryId=b.AnimalCategoryId}).ToListAsync();
            return Ok(breeds);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get(string search)
        {

            var breeds= await _context.Breeds.Include(b => b.AnimalCategory).Where(b=>b.Name.Contains(search)).ToListAsync();   
           var BreedDto =breeds.Select(b=>b.ToBreedDto()).ToList();
            return Ok(BreedDto);
        }
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {

            var breed = await _context.Breeds.Include(b=>b.AnimalCategory).FirstOrDefaultAsync(b=>b.Id==id);
            var breeddto=breed.ToBreedDto();
            return Ok(breeddto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateBreed([FromBody] CreateBreedDto breeddto)
        {
            var BreedModel = breeddto.CreateBreedDto();
            await _context.AddAsync(BreedModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = BreedModel.Id }, BreedModel.Name);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteBreed(int id)
        {

            var BreedModel = await _context.Breeds.FirstOrDefaultAsync(b => b.Id == id);
            if (BreedModel == null) { return NotFound(); }
            _context.Remove(BreedModel);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateBreed(int id, [FromForm] CreateBreedDto createBreedDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var breedmodel = await _context.Breeds.FirstOrDefaultAsync(b => b.Id == id);
            if (breedmodel == null) { return NotFound(); }

            breedmodel.Name = createBreedDto.Name;
            breedmodel.AnimalCategoryId = createBreedDto.AnimalCategoryId;
            await _context.SaveChangesAsync();


            return Ok(breedmodel.ToBreedDto());
        }
    }
}
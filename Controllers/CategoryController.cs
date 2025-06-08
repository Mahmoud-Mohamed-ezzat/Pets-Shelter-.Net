using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Animal2.Dto.Category;
using Animal2.Mapper;
using Animal2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Animal2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase   //this controller authorized to Admin Only 
    {

        private readonly AnimalsContext _context;

        public CategoryController(AnimalsContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet ("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            var Categories =await _context.AnimalCategories.ToListAsync();
            var categoriesDto = Categories.Select(a => a.toCategoryDto2());
            return Ok(categoriesDto);
        }

        [Authorize]
        [HttpGet("Get")]
        public async Task<IActionResult> Get(string search) // retrieve All Categories 
        {

        var categories =await _context.AnimalCategories.Include(a=>a.Breed).Include(a=>a.Animal).Where(c=>c.CategoryName.Contains(search)).ToListAsync();
            var categoriesDto = categories.Select(a => a.toCategoryDto());

            return Ok(categoriesDto);

        }
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetID(int id) // retrieve All Categories 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await _context.AnimalCategories.Include(c=>c.Breed).Include(b=>b.Animal).FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) { return NotFound(); }
            var categoryDto = category.toCategoryDto();
            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto category) // Create Category 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var categorymodel = category.CreateCategoryDto();
            await _context.AnimalCategories.AddAsync(categorymodel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetID), new { id = categorymodel.Id }, categorymodel.toCategoryDto());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id) // Delete Category
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await _context.AnimalCategories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) { return NotFound(); }
            _context.AnimalCategories.Remove(category);
            await _context.SaveChangesAsync();
            return Ok();

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CreateCategoryDto categoryDto) // Update Category
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await _context.AnimalCategories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) { return NotFound(); }

            category.CategoryName = categoryDto.CategoryName;
            await _context.SaveChangesAsync();
            return Ok(category.toCategoryDto());
        }

    }
}

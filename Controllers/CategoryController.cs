using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Animal2.Dto.Category;
using Animal2.Mapper;
using Animal2.Models;
using Microsoft.EntityFrameworkCore;

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
        //[HttpGet]
        //public async Task<IActionResult> GetForSearch(string search)
        //{

        //    var Categories= _context.AnimalCategories.AsQueryable();
        //    Categories = Categories.Where(b => b.CategoryName.Contains(search));
        //    return Ok(Categories.ToListAsync());
        //}
            [HttpGet]
        public async Task<IActionResult> Get(string search) // retrieve All Categories 
        {
            
            var categories =await _context.AnimalCategories.Include(a=>a.Breed).Where(c=>c.CategoryName.Contains(search)).ToListAsync();
            var categoriesDto = categories.Select(a => a.toCategoryDto());

            return Ok(categories);

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetID(int id) // retrieve All Categories 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await _context.AnimalCategories.Include(c=>c.Breed).FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) { return NotFound(); }
            return Ok(category);

        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto category) // Create Category 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var categorymodel = category.CreateCategoryDto();
            await _context.AnimalCategories.AddAsync(categorymodel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = categorymodel.Id }, categorymodel.toCategoryDto());
        }

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

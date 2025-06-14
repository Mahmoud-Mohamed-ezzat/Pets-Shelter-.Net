using Microsoft.AspNetCore.Mvc;
using Animal2.Dto.Category;
using Animal2.Mapper;
using Animal2.Models;
using Microsoft.AspNetCore.Authorization;
using Animal2.Interface;

namespace Animal2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase   //this controller authorized to Admin Only 
    {
        private readonly AnimalsContext _context;
        private readonly ICategory _CategoryRepo;
        public CategoryController(AnimalsContext context, ICategory CategoryRepo)
        {
            _context = context;
            _CategoryRepo = CategoryRepo;
        }
        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var Categories = await _CategoryRepo.GetAllAsync();
            return Ok(Categories);
        }

        [Authorize]
        [HttpGet("Get")]
        public async Task<IActionResult> Get(string search) // retrieve All Categories 
        {
            var categories = await _CategoryRepo.GetALLBySearch(search);
            return Ok(categories);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetID(int id) // retrieve All Categories 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await _CategoryRepo.GetOneAsync(id);
            if (category == null) { return NotFound(); }
            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto category) // Create Category 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var categorymodel = await _CategoryRepo.CreateCategory(category);
            return CreatedAtAction(nameof(GetID), new { id = categorymodel.Id }, categorymodel.toCategoryDto());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id) // Delete Category
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await _CategoryRepo.DeleteCategory(id);
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CreateCategoryDto categoryDto) // Update Category
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await _CategoryRepo.UpdateCategory(id, categoryDto);
            return Ok(category.toCategoryDto());
        }
    }
}

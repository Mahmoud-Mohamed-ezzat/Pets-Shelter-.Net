using Animal2.Dto.Category;
using Animal2.Interface;
using Animal2.Mapper;
using Animal2.Models;
using Microsoft.EntityFrameworkCore;

namespace Animal2.Helper
{
    public class CategoryRepo : ICategory
    {
        private readonly AnimalsContext _context;

        public CategoryRepo(AnimalsContext context)
        {
            _context = context;
        }
        public async Task<AnimalCategory> CreateCategory(CreateCategoryDto category)
        {
            var categorymodel = category.CreateCategoryDto();
            await _context.AnimalCategories.AddAsync(categorymodel);
            await _context.SaveChangesAsync();
            return categorymodel;
        }

        public async Task<AnimalCategory> DeleteCategory(int id)
        {
            var category = await _context.AnimalCategories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) { return null; }
            _context.AnimalCategories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var Categories = await _context.AnimalCategories.ToListAsync();
            var categoriesDto = Categories.Select(a => a.toCategoryDto2());
            return categoriesDto.ToList();
        }
        public async Task<List<CategoryDto>> GetALLBySearch(string Search)
        {
            var categories = await _context.AnimalCategories.Include(a => a.Breed).Include(a => a.Animal).Where(c => c.CategoryName.ToLower().Contains(Search.ToLower())).ToListAsync();
            var categoriesDto = categories.Select(a => a.toCategoryDto());
            return categoriesDto.ToList();
        }

        public async Task<AnimalCategory> GetOneAsync(int id)
        {
            var category = await _context.AnimalCategories.Include(c => c.Breed).Include(b => b.Animal).FirstOrDefaultAsync(c => c.Id == id);
            return category;       
        }


        public async Task<AnimalCategory> UpdateCategory(int id,CreateCategoryDto categoryDto)
        {
            var category = await _context.AnimalCategories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) { return null; }
            category.CategoryName = categoryDto.CategoryName;
            await _context.SaveChangesAsync();
            return category;
        }
    }
}

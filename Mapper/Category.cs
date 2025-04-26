using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Animal2.Dto.Category;
using Animal2.Models;

namespace Animal2.Mapper
{
    public static class Category
    {

        public static CategoryDto toCategoryDto(this AnimalCategory category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Breeds = category.Breed.Select(b=>b.ToBreedDto()).ToList(),
            };

        }
        public static AnimalCategory CreateCategoryDto(this CreateCategoryDto category)
        {
            return new AnimalCategory
            {
                CategoryName = category.CategoryName
            };

        }
    }
}

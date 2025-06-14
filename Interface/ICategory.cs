using System;
using Animal2.Dto.Category;
using Animal2.Models;
using Microsoft.AspNetCore.Mvc;


namespace Animal2.Interface
{
    public interface ICategory
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<List<CategoryDto>> GetALLBySearch(string Search);
        Task<AnimalCategory> GetOneAsync(int id);
        Task<AnimalCategory> CreateCategory(CreateCategoryDto category);
        Task<AnimalCategory> DeleteCategory(int id);
        Task<AnimalCategory> UpdateCategory(int id,CreateCategoryDto categoryDto);
    }
}

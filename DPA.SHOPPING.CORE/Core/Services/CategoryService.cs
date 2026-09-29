using DPA.SHOPPING.CORE.Core.DTOs;
using DPA.SHOPPING.CORE.Core.Entities;
using DPA.SHOPPING.CORE.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DPA.SHOPPING.CORE.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryListDTO>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();
            var categoryListDTOs = new List<CategoryListDTO>();

            foreach (var category in categories)
            {
                categoryListDTOs.Add(new CategoryListDTO
                {
                    Id = category.Id,
                    Description = category.Description
                });
            }

            return categoryListDTOs;
        }

        //Get Category by Id
        public async Task<CategoryListDTO> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return null;
            }

            return new CategoryListDTO
            {
                Id = category.Id,
                Description = category.Description
            };
        }

        //Create Category
        public async Task<bool> CreateCategory(CategoryCreateDTO categoryDTO)
        {
            var category = new Category
            {
                Description = categoryDTO.Description,
                IsActive = true
            };
            return await _categoryRepository.CreateCategory(category);
        }
        //Update Category
        public async Task<bool> UpdateCategory(CategoryUpdateDTO categoryDTO)
        {
            var category = await _categoryRepository.GetCategoryById(categoryDTO.Id);
            if (category == null)
            {
                return false;
            }
            category.Description = categoryDTO.Description;
            return await _categoryRepository.UpdateCategory(category);
        }

        //Delete Category
        public async Task<bool> DeleteCategory(CategoryDeleteDTO categoryDTO)
        {
            return await _categoryRepository.DeleteCategory(categoryDTO.Id);
        }


    }
}

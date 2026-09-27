using DPA.SHOPPING.CORE.Core.DTOs;
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


    }
}

using DPA.SHOPPING.CORE.Core.DTOs;

namespace DPA.SHOPPING.CORE.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryListDTO>> GetCategories();
        Task<CategoryListDTO> GetCategoryById(int id);
        Task<bool> CreateCategory(CategoryCreateDTO categoryDTO);
        Task<bool> UpdateCategory(CategoryUpdateDTO categoryDTO);
        Task<bool> DeleteCategory(CategoryDeleteDTO categoryDTO);
    }
}
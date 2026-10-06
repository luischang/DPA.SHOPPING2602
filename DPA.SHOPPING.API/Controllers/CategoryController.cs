using DPA.SHOPPING.CORE.Core.DTOs;
using DPA.SHOPPING.CORE.Core.Entities;
using DPA.SHOPPING.CORE.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DPA.SHOPPING.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [EndpointSummary("Get all categories")]
        [EndpointDescription("Returns a list of all categories in the system.")]
        public async Task<IActionResult> GetCategories()
        {
            //var categories = await _categoryRepository.GetCategories();
            var categories = await _categoryService.GetCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [EndpointSummary("Get category by ID")]

        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        [EndpointSummary("Create a new category")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO category)
        {
            var result = await _categoryService.CreateCategory(category);
            if (!result)
                return BadRequest("Failed to create category.");
            return NoContent();
        }

        [HttpPut("{id}")]
        [EndpointSummary("Update an existing category")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDTO category)
        {
            if(id != category.Id)
                return BadRequest("Category ID mismatch.");

            var result = await _categoryService.UpdateCategory(category);
            if (!result)
                return NotFound();

            return Ok(category);
        }
        
        [HttpDelete("{id}")]
        [EndpointSummary("Delete a category")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            CategoryDeleteDTO deleteDTO = new CategoryDeleteDTO { Id = id };
            var result = await _categoryService.DeleteCategory(deleteDTO);
            if (!result)
                return NotFound();
            return NoContent();
        }

    }
}

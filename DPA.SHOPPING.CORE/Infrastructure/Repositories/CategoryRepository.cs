using DPA.SHOPPING.CORE.Core.Entities;
using DPA.SHOPPING.CORE.Core.Interfaces;
using DPA.SHOPPING.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DPA.SHOPPING.CORE.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDbContext _context;

        public CategoryRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await _context
                                    .Category
                                    .Where(c => c.IsActive == true)
                                    .ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context
                                    .Category
                                    .Where(c => c.Id == id && c.IsActive == true)
                                    .FirstOrDefaultAsync();
            return category;
        }

        public async Task<bool> CreateCategory(Category category)
        {
            await _context.Category.AddAsync(category);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        // Update Category
        public async Task<bool> UpdateCategory(Category category)
        {
            _context.Category.Update(category);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        // Delete soft category
        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return false;
            }
            category.IsActive = false;
            _context.Category.Update(category);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}

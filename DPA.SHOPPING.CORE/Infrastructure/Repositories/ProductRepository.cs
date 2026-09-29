using DPA.SHOPPING.CORE.Core.Entities;
using DPA.SHOPPING.CORE.Core.Interfaces;
using DPA.SHOPPING.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DPA.SHOPPING.CORE.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;

        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _context
                                    .Product
                                    .Include(p => p.Category)
                                    .Where(p => p.IsActive == true)
                                    .ToListAsync();
            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _context
                                    .Product
                                    .Include(p => p.Category)
                                    .Where(p => p.Id == id && p.IsActive == true)
                                    .FirstOrDefaultAsync();
            return product;
        }

        public async Task<bool> CreateProduct(Product product)
        {
            await _context.Product.AddAsync(product);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            _context.Product.Update(product);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            product.IsActive = false;
            _context.Product.Update(product);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}

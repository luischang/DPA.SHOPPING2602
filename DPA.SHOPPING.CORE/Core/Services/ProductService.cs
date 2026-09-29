using DPA.SHOPPING.CORE.Core.DTOs;
using DPA.SHOPPING.CORE.Core.Entities;
using DPA.SHOPPING.CORE.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DPA.SHOPPING.CORE.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductListDTO>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            var productListDTOs = new List<ProductListDTO>();

            foreach (var product in products)
            {
                productListDTOs.Add(new ProductListDTO
                {
                    Id = product.Id,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    ImageUrl = product.ImageUrl,
                    Category = product.Category == null ? null : new CategoryListDTO
                    {
                        Id = product.Category.Id,
                        Description = product.Category.Description
                    }
                });
            }

            return productListDTOs;
        }

        public async Task<ProductListDTO> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return null;
            }

            return new ProductListDTO
            {
                Id = product.Id,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
                Category = product.Category == null ? null : new CategoryListDTO
                {
                    Id = product.Category.Id,
                    Description = product.Category.Description
                }
            };
        }

        public async Task<bool> CreateProduct(ProductCreateDTO productDTO)
        {
            var product = new Product
            {
                Description = productDTO.Description,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                ImageUrl = productDTO.ImageUrl,
                CategoryId = productDTO.CategoryId,
                IsActive = true
            };
            return await _productRepository.CreateProduct(product);
        }

        public async Task<bool> UpdateProduct(ProductUpdateDTO productDTO)
        {
            var product = await _productRepository.GetProductById(productDTO.Id);
            if (product == null)
            {
                return false;
            }
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;
            product.Stock = productDTO.Stock;
            product.ImageUrl = productDTO.ImageUrl;
            product.CategoryId = productDTO.CategoryId;
            return await _productRepository.UpdateProduct(product);
        }

        public async Task<bool> DeleteProduct(ProductDeleteDTO productDTO)
        {
            return await _productRepository.DeleteProduct(productDTO.Id);
        }
    }
}

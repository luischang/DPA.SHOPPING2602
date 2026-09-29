using DPA.SHOPPING.CORE.Core.Entities;

namespace DPA.SHOPPING.CORE.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> CreateProduct(Product product);
        Task<bool> DeleteProduct(int id);
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<bool> UpdateProduct(Product product);
    }
}

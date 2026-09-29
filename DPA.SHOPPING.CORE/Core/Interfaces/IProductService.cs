using DPA.SHOPPING.CORE.Core.DTOs;

namespace DPA.SHOPPING.CORE.Core.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListDTO>> GetProducts();
        Task<ProductListDTO> GetProductById(int id);
        Task<bool> CreateProduct(ProductCreateDTO productDTO);
        Task<bool> UpdateProduct(ProductUpdateDTO productDTO);
        Task<bool> DeleteProduct(ProductDeleteDTO productDTO);
    }
}

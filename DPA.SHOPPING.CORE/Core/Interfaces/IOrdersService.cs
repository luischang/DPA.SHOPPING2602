using DPA.SHOPPING.CORE.Core.DTOs;
using System.Collections.Generic;

namespace DPA.SHOPPING.CORE.Core.Interfaces
{
    public interface IOrdersService
    {
        Task<IEnumerable<OrderResponseDTO>> GetOrders();
        Task<OrderResponseDTO> GetOrderById(int id);
        Task<bool> CreateOrder(OrderCreateDTO orderDTO);
    }
}

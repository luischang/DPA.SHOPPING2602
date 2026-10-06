using DPA.SHOPPING.CORE.Core.Entities;
using System.Collections.Generic;

namespace DPA.SHOPPING.CORE.Core.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(int orderId);
        Task<OrderDetail> GetOrderDetailById(int id);
        Task<bool> CreateOrderDetail(OrderDetail detail);
    }
}

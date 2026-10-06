using DPA.SHOPPING.CORE.Core.Entities;
using System.Collections.Generic;

namespace DPA.SHOPPING.CORE.Core.Interfaces
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Orders>> GetOrders();
        Task<Orders> GetOrderById(int id);
        Task<bool> CreateOrder(Orders order);
    }
}

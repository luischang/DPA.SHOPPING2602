using DPA.SHOPPING.CORE.Core.Entities;
using DPA.SHOPPING.CORE.Core.Interfaces;
using DPA.SHOPPING.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DPA.SHOPPING.CORE.Infrastructure.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly StoreDbContext _context;

        public OrdersRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Orders>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetail)
                    .ThenInclude(d => d.Product)
                        .ThenInclude(p => p.Category)
                .ToListAsync();
            return orders;
        }

        public async Task<Orders> GetOrderById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetail)
                    .ThenInclude(d => d.Product)
                        .ThenInclude(p => p.Category)
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();
            return order;
        }

        public async Task<bool> CreateOrder(Orders order)
        {
            await _context.Orders.AddAsync(order);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}

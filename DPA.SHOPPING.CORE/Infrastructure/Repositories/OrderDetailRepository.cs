using DPA.SHOPPING.CORE.Core.Entities;
using DPA.SHOPPING.CORE.Core.Interfaces;
using DPA.SHOPPING.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DPA.SHOPPING.CORE.Infrastructure.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly StoreDbContext _context;

        public OrderDetailRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(int orderId)
        {
            var details = await _context.OrderDetail
                .Include(d => d.Product)
                    .ThenInclude(p => p.Category)
                .Where(d => d.OrdersId == orderId)
                .ToListAsync();
            return details;
        }

        public async Task<OrderDetail> GetOrderDetailById(int id)
        {
            var detail = await _context.OrderDetail
                .Include(d => d.Product)
                    .ThenInclude(p => p.Category)
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
            return detail;
        }

        public async Task<bool> CreateOrderDetail(OrderDetail detail)
        {
            await _context.OrderDetail.AddAsync(detail);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}

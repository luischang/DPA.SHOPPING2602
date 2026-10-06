using DPA.SHOPPING.CORE.Core.DTOs;
using DPA.SHOPPING.CORE.Core.Entities;
using DPA.SHOPPING.CORE.Core.Interfaces;
using DPA.SHOPPING.CORE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DPA.SHOPPING.CORE.Core.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly StoreDbContext _context;

        public OrdersService(
            IOrdersRepository ordersRepository,
            IOrderDetailRepository orderDetailRepository,
            IProductRepository productRepository,
            StoreDbContext context)
        {
            _ordersRepository = ordersRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _context = context;
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetOrders()
        {
            var orders = await _ordersRepository.GetOrders();
            var result = orders.Select(o => new OrderResponseDTO
            {
                Id = o.Id,
                CreatedAt = o.CreatedAt,
                UserId = o.UserId,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                OrderDetail = o.OrderDetail?.Select(d => new OrderDetailResponseDTO
                {
                    Id = d.Id,
                    ProductId = d.ProductId,
                    Product = d.Product == null ? null : new ProductListDTO
                    {
                        Id = d.Product.Id,
                        Description = d.Product.Description,
                        Price = d.Product.Price,
                        Stock = d.Product.Stock,
                        ImageUrl = d.Product.ImageUrl,
                        Category = d.Product.Category == null ? null : new CategoryListDTO
                        {
                            Id = d.Product.Category.Id,
                            Description = d.Product.Category.Description
                        }
                    },
                    Quantity = d.Quantity,
                    Price = d.Price
                }).ToList()
            });

            return result;
        }

        public async Task<OrderResponseDTO> GetOrderById(int id)
        {
            var o = await _ordersRepository.GetOrderById(id);
            if (o == null) return null;

            var dto = new OrderResponseDTO
            {
                Id = o.Id,
                CreatedAt = o.CreatedAt,
                UserId = o.UserId,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                OrderDetail = o.OrderDetail?.Select(d => new OrderDetailResponseDTO
                {
                    Id = d.Id,
                    ProductId = d.ProductId,
                    Product = d.Product == null ? null : new ProductListDTO
                    {
                        Id = d.Product.Id,
                        Description = d.Product.Description,
                        Price = d.Product.Price,
                        Stock = d.Product.Stock,
                        ImageUrl = d.Product.ImageUrl,
                        Category = d.Product.Category == null ? null : new CategoryListDTO
                        {
                            Id = d.Product.Category.Id,
                            Description = d.Product.Category.Description
                        }
                    },
                    Quantity = d.Quantity,
                    Price = d.Price
                }).ToList()
            };

            return dto;
        }

        public async Task<bool> CreateOrder(OrderCreateDTO orderDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = new Orders
                {
                    CreatedAt = DateTime.UtcNow,
                    UserId = orderDTO.UserId,
                    Status = string.IsNullOrEmpty(orderDTO.Status) ? "N" : orderDTO.Status
                };

                var created = await _ordersRepository.CreateOrder(order);
                if (!created)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                decimal total = 0;
                foreach (var item in orderDTO.OrderDetail)
                {
                    var product = await _productRepository.GetProductById(item.ProductId);
                    var price = product?.Price ?? 0;
                    var detail = new OrderDetail
                    {
                        OrdersId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = price,
                        CreatedAt = DateTime.UtcNow
                    };

                    var dcreated = await _orderDetailRepository.CreateOrderDetail(detail);
                    if (!dcreated)
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }

                    total += (price * item.Quantity);
                }

                // update order total
                order.TotalAmount = total;
                var updateResult = await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return updateResult > 0;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}

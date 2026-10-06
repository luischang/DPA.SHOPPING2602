using System;
using System.Collections.Generic;

namespace DPA.SHOPPING.CORE.Core.DTOs
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UserId { get; set; }
        public string Status { get; set; }
        public decimal? TotalAmount { get; set; }
        public IEnumerable<OrderDetailResponseDTO> OrderDetail { get; set; }
    }

    public class OrderDetailResponseDTO
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public ProductListDTO Product { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }

    public class OrderCreateDTO
    {
        public int UserId { get; set; }
        public string Status { get; set; }
        public IEnumerable<OrderDetailCreateDTO> OrderDetail { get; set; }
    }

    public class OrderDetailCreateDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

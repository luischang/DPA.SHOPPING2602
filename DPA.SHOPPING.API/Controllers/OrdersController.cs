using DPA.SHOPPING.CORE.Core.DTOs;
using DPA.SHOPPING.CORE.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DPA.SHOPPING.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _ordersService.GetOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _ordersService.GetOrderById(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO order)
        {
            var result = await _ordersService.CreateOrder(order);
            if (!result) return BadRequest("Failed to create order");
            return NoContent();
        }
    }
}

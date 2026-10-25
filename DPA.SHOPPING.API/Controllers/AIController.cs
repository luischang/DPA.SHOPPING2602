using DPA.SHOPPING.CORE.Core.DTOs;
using DPA.SHOPPING.CORE.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;

namespace DPA.SHOPPING.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly IChatClient _chatClient;
        private readonly IOrdersService _ordersService;

        public AIController(IChatClient chatClient, IOrdersService ordersService)
        {
            _chatClient = chatClient;
            _ordersService = ordersService;
        }
        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] ChatRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest("Message cannot be empty.");
            }

            var response = await _chatClient.GetResponseAsync(request.Message);
            var chatResponse = new ChatResponseDTO
            {
                Answer = response.Text,
                Model = response.ModelId
            };
            return Ok(chatResponse);
        }

        [HttpPost("my-orders")]
        public async Task<IActionResult> MyOrders([FromBody] MyOrdersRequestDTO request)
        {
            // Transactional logic to retrieve orders for the user
            var orders = await _ordersService.GetOrderByUserId(request.UserId);

            //contextual logic to filter orders based on the message content
            var context = $"User {request.UserId} has {orders?.OrderDetail?.Count() ?? 0} orders " +
                $"with Total Amount: {orders?.TotalAmount ?? 0} with Products (Orders->OrderDetails-Products) " +
                $": {string.Join(", ", orders?.OrderDetail?.Select(od => od.Product.Description) ?? Enumerable.Empty<string>())}";

            if(context.Length == 0)
            {
                return NotFound("No orders found for the user.");
            }

            var messages = new List<ChatMessage>
            {
                new ChatMessage(ChatRole.System, "Eres el asistente de DPA SHOPPING, " +
                "Responde a las preguntas sobre los pedidos del cliente en español. Si la respesta no está en esos datos, indica que no tienes información suficiente.\n\n" +
                "Pedido del cliente:\n" + context),
                new ChatMessage(ChatRole.User, request.Message),                
            };
            var response = await _chatClient.GetResponseAsync(messages);
            var respondeDTO = new MyOrdersResponseDTO
            {
                Answer = response.Text,
                Model = response.ModelId,
                Context = context
            };

            return Ok(respondeDTO);
        }
    }
}

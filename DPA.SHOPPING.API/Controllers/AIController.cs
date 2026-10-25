using DPA.SHOPPING.CORE.Core.DTOs;
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

        public AIController(IChatClient chatClient)
        {
            _chatClient = chatClient;
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
    }
}

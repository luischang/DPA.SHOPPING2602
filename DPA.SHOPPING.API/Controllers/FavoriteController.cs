using DPA.SHOPPING.CORE.Core.DTOs;
using DPA.SHOPPING.CORE.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DPA.SHOPPING.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet]
        [EndpointSummary("Get all favorites grouped by user")]
        public async Task<IActionResult> GetFavorites()
        {
            var favorites = await _favoriteService.GetFavorites();
            return Ok(favorites);
        }

        [HttpGet("user/{userId}")]
        [EndpointSummary("Get favorites for a specific user")]
        public async Task<IActionResult> GetFavoritesByUser(int userId)
        {
            var favorites = await _favoriteService.GetFavoritesByUserId(userId);
            if (favorites == null)
                return NotFound();
            return Ok(favorites);
        }

        [HttpPost]
        [EndpointSummary("Create a new favorite")]
        public async Task<IActionResult> CreateFavorite([FromBody] FavoriteCreateDTO favorite)
        {
            var result = await _favoriteService.CreateFavorite(favorite);
            if (!result)
                return BadRequest("Failed to create favorite.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Delete a favorite")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var deleteDTO = new FavoriteDeleteDTO { Id = id };
            var result = await _favoriteService.DeleteFavorite(deleteDTO);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}

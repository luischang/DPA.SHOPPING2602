using System.Threading.Tasks;
using DPA.SHOPPING.CORE.Core.DTOs;
using DPA.SHOPPING.CORE.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DPA.SHOPPING.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDto dto)
        {
            var result = await _userService.SignInAsync(dto);
            if (result == null || result.User == null)
                return Unauthorized();

            return Ok(result);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDto dto)
        {
            var created = await _userService.SignUpAsync(dto);
            if (created == null)
                return Conflict(new { message = "Email already exists" });

            return Ok(created);
        }
    }
}

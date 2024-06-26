using Microsoft.AspNetCore.Mvc;
using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Contracts.User;

namespace EasyLibrary.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public AuthController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            try
            {
                var token = await _usersService.LoginUser(request);
                HttpContext.Response.Cookies.Append("tasty-bebra", token);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            try
            {
                var userId = await _usersService.CreateUser(request);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

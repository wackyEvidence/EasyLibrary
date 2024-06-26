using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Contracts.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace EasyLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService; 
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserResponseFull>> GetUserById(Guid id)
        {
            try
            {
                var user = await _usersService.GetUserById(id);
                var response = new UserResponseFull(
                        user.Id,
                        user.Name,
                        user.Surname,
                        user.Patronymic,
                        user.PassportNumber,
                        user.PassportSeries,
                        user.BirthDate,
                        user.RegistrationDate,
                        user.PhoneNumber,
                        user.Email,
                        user.IsAdmin
                    );

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);   
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<UserResponseDisplay>>> GetAllUsers()
        {
            var users = await _usersService.GetAllUsers();

            if (Request.Headers.TryGetValue("Type", out StringValues typeHeader))
            {
                if (typeHeader == "full")
                {
                    var response = users.Select(u => new UserResponseFull(
                        u.Id,
                        u.Name,
                        u.Surname,
                        u.Patronymic,
                        u.PassportNumber,
                        u.PassportSeries,
                        u.BirthDate,
                        u.RegistrationDate,
                        u.PhoneNumber,
                        u.Email,
                        u.IsAdmin
                    ));

                    return Ok(response);
                }
                else if (typeHeader == "display")
                {
                    var response = users.Select(u => new UserResponseDisplay(
                        u.Id,
                        u.Name,
                        u.Surname,
                        u.Patronymic,
                        u.Email,
                        u.RegistrationDate,
                        u.IsAdmin
                    ));

                    return Ok(response);
                }
                else
                    return BadRequest("Unexpected \"Type\" header value");
            }
            else
                return BadRequest("Expected \"Type\" header");
        }

        [HttpGet("stats")]
        public async Task<ActionResult<UserRegistrationStatsResponseItem>> GetUserRegistrationStats([FromQuery] string type)
        {
            switch (type)
            {
                case "registrations":
                    var stats = await _usersService.GetUserRegistrationStats();
                    return Ok(stats);
                default:
                    return BadRequest("invalid stats type parameter");
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateUser(Guid id, [FromBody] UserRequest request)
        {
            var userId = await _usersService.UpdateUser(
                id,
                request.Name,
                request.Surname,
                request.Patronymic,
                request.PassportNumber,
                request.PassportSeries,
                request.BirthDate,
                request.Email,
                request.PhoneNumber,
                request.IsAdmin
            );

            return Ok(userId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteUser(Guid id)
        {
            return Ok(await _usersService.DeleteUser(id));
        }
    }
}

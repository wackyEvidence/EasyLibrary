using EasyLibrary.API.Contracts;
using EasyLibrary.Application.Services;
using Microsoft.AspNetCore.Mvc;
using EasyLibrary.Core.Models;

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

        [HttpGet] 
        public async Task<ActionResult<List<UserResponse>>> GetAllUsers()
        {
            var users = await _usersService.GetAllUsers();
            var response = users.Select(u => new UserResponse(u.Id, u.Name, u.Surname, u.Patronymic, u.Email));
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] UserRequest request)
        {
            try
            {
                var user = Core.Models.User.Create(
                    Guid.NewGuid(), 
                    request.Name, 
                    request.Surname, 
                    request.Patronymic, 
                    request.PassportNumber,
                    request.PassportSeries,
                    request.BirthDate, 
                    DateOnly.Parse(DateTime.Now.ToShortDateString()),
                    request.Email, 
                    request.PhoneNumber,
                    request.IsAdmin
                    );

                var userId = await _usersService.CreateUser(user);

                return Ok(userId);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
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

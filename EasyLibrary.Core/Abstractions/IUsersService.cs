using EasyLibrary.Core.Contracts.User;
using EasyLibrary.Core.Models;

namespace EasyLibrary.Core.Abstractions
{
    public interface IUsersService
    {
        Task<List<UserRegistrationStatsResponseItem>> GetUserRegistrationStats();
        Task<string> LoginUser(LoginUserRequest request);
        Task<Guid> CreateUser(RegisterUserRequest request);
        Task<Guid> DeleteUser(Guid id);
        Task<User> GetUserById(Guid id);
        Task<List<User>> GetAllUsers();
        Task<Guid> UpdateUser(Guid id, string name, string? surname, string? patronymic, string? passportNumber, string? passportSeries, DateOnly? birthDate, string email, string? phoneNumber, bool isAdmin);
    }
}
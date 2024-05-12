using EasyLibrary.Core.Models;

namespace EasyLibrary.Application.Services
{
    public interface IUsersService
    {
        Task<Guid> CreateUser(User user);
        Task<Guid> DeleteBook(Guid id);
        Task<List<User>> GetAllUsers();
        Task<Guid> UpdateBook(Guid id, string name, string surname, string patronymic, string passportNumber, string passportSeries, DateOnly birthDate, string? email, string phoneNumber, bool isAdmin);
    }
}
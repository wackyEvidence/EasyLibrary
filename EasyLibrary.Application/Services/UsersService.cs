using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Repositories;

namespace EasyLibrary.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _usersRepository.Get();
        }

        public async Task<Guid> CreateUser(User user)
        {
            return await _usersRepository.Create(user);
        }

        public async Task<Guid> UpdateBook(Guid id, string name, string surname, string patronymic, string passportNumber,
            string passportSeries, DateOnly birthDate, string? email, string phoneNumber, bool isAdmin)
        {
            return await _usersRepository.Update(id, name, surname, patronymic, passportNumber, passportSeries, birthDate, email, phoneNumber, isAdmin);
        }

        public async Task<Guid> DeleteBook(Guid id)
        {
            return await _usersRepository.Delete(id);
        }
    }
}

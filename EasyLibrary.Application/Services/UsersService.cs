using EasyLibrary.Application.Exceptions;
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

        public async Task<User> GetUserById(Guid id)
        {
            var user =  await _usersRepository.GetById(id);

            return user == null ? 
                throw new UserNotFoundException("User with provided id was not found") : 
                user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _usersRepository.Get();
        }

        public async Task<Guid> CreateUser(User user)
        {
            return await _usersRepository.Create(user);
        }

        public async Task<Guid> UpdateUser(Guid id, string name, string surname, string? patronymic, string passportNumber,
            string passportSeries, DateOnly birthDate, string email, string phoneNumber, bool isAdmin)
        {
            return await _usersRepository.Update(id, name, surname, patronymic, passportNumber, passportSeries, birthDate, email, phoneNumber, isAdmin);
        }

        public async Task<Guid> DeleteUser(Guid id)
        {
            return await _usersRepository.Delete(id);
        }
    }
}

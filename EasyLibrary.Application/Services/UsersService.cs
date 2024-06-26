using EasyLibrary.Application.Exceptions;
using EasyLibrary.Core.Models;
using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Contracts.User;
using EasyLibrary.Core.Abstractions.Auth;

namespace EasyLibrary.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public UsersService(
            IUsersRepository usersRepository, 
            IPasswordHasher passwordHasher, 
            IJwtProvider jwtProvider
        )
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;   
            _jwtProvider = jwtProvider;
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await _usersRepository.GetById(id);

            return user ?? throw new NotFoundException<User>(id);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _usersRepository.Get();
        }

        public async Task<List<UserRegistrationStatsResponseItem>> GetUserRegistrationStats()
        {
            var users = await _usersRepository.Get();
            return users.GroupBy(u => u.RegistrationDate)
                             .Select(g => new UserRegistrationStatsResponseItem(g.Key, g.Count()))
                             .ToList();
        }

        public async Task<string> LoginUser(LoginUserRequest request)
        {
            var user = await _usersRepository.GetByEmail(request.Email);

            var result = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if (!result)
                throw new Exception("invalid password entered");

            return _jwtProvider.GenerateToken(user);
        }

        public async Task<Guid> CreateUser(RegisterUserRequest request)
        {
            var hashedPassword = _passwordHasher.Generate(request.Password);
            var user = User.Create(Guid.NewGuid(), request.Name, request.Email, hashedPassword);

            return await _usersRepository.Create(user);
        }

        public async Task<Guid> UpdateUser(Guid id, string name, string? surname, string? patronymic, string? passportNumber,
            string? passportSeries, DateOnly? birthDate, string email, string? phoneNumber, bool isAdmin)
        {
            return await _usersRepository.Update(id, name, surname, patronymic, passportNumber, passportSeries, birthDate, email, phoneNumber, isAdmin);
        }

        public async Task<Guid> DeleteUser(Guid id)
        {
            return await _usersRepository.Delete(id);
        }
    }
}

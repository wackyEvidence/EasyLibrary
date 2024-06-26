using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace EasyLibrary.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly EasyLibraryDbContext _context;
        private readonly IMapper<UserEntity, User> _userMapper;

        public UsersRepository(EasyLibraryDbContext context, IMapper<UserEntity, User> userMapper)
        {
            _context = context;
            _userMapper = userMapper;
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync() ?? throw new Exception();

            return _userMapper.Map(userEntity);
        }

        public async Task<User?> GetById(Guid id)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            return userEntity == null ?
                null :
                _userMapper.Map(userEntity);
        }

        public async Task<List<User>> Get()
        {
            var userEntities = await _context.Users.AsNoTracking().ToListAsync();

            return userEntities.ConvertAll(_userMapper.Map);
        }

        public async Task<Guid> Create(User user)
        {
            var userEntity = new UserEntity
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                PassportNumber = user.PassportNumber,
                PassportSeries = user.PassportSeries,
                BirthDate = user.BirthDate,
                RegistrationDate = user.RegistrationDate,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                IsAdmin = user.IsAdmin
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.Id;
        }

        public async Task<Guid> Update(
        Guid id, string name, string? surname, string? patronymic, string? passportNumber,
            string? passportSeries, DateOnly? birthDate, string email, string? phoneNumber, bool isAdmin)
        {
            await _context.Users
                    .Where(u => u.Id == id)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(u => u.Name, name)
                        .SetProperty(u => u.Surname, surname)
                        .SetProperty(u => u.Patronymic, patronymic)
                        .SetProperty(u => u.PassportNumber, passportNumber)
                        .SetProperty(u => u.PassportSeries, passportSeries)
                        .SetProperty(u => u.BirthDate, birthDate)
                        .SetProperty(u => u.Email, email)
                        .SetProperty(u => u.PhoneNumber, phoneNumber)
                        .SetProperty(u => u.IsAdmin, isAdmin)
                    );

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}

using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace EasyLibrary.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly EasyLibraryDbContext _context;

        public UsersRepository(EasyLibraryDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetById(Guid id)
        {
            var user = await _context.Users.AsNoTracking().Where(u => u.Id == id).FirstOrDefaultAsync();
            
            return user == null?
                null : 
                User.Create(user.Id, user.Name, user.Surname, user.Patronymic, user.PassportNumber,
                    user.PassportSeries, user.BirthDate, user.RegistrationDate, user.Email,
                    user.PhoneNumber, user.IsAdmin);
        }

        public async Task<List<User>> Get()
        {
            var userEntities = await _context.Users.AsNoTracking().ToListAsync();

            var users = userEntities
                .Select(u => User.Create(
                    u.Id, u.Name, u.Surname, u.Patronymic, u.PassportNumber,
                    u.PassportSeries, u.BirthDate, u.RegistrationDate, u.Email,
                    u.PhoneNumber, u.IsAdmin))
                .ToList();

            return users;
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
                PhoneNumber = user.PhoneNumber,
                IsAdmin = user.IsAdmin
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.Id;
        }

        public async Task<Guid> Update(
        Guid id, string name, string surname, string? patronymic, string passportNumber,
            string passportSeries, DateOnly birthDate, string email, string phoneNumber, bool isAdmin)
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
                        .SetProperty(u => u.IsAdmin, isAdmin));
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

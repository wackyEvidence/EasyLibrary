using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;

namespace EasyLibrary.DataAccess.Mappers
{
    public class UserMapper : IMapper<UserEntity, User>
    {
        public User Map(UserEntity source)
        {
            return User.Create(
                source.Id,
                source.Name,
                source.Surname,
                source.Patronymic, 
                source.PassportNumber,
                source.PassportSeries,
                source.BirthDate,
                source.RegistrationDate,
                source.Email,
                source.PasswordHash,
                source.PhoneNumber,
                source.IsAdmin,
                new List<BookIssuance>()
            );
        }
    }
}

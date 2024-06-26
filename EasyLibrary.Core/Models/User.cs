using System.ComponentModel.Design.Serialization;

namespace EasyLibrary.Core.Models
{
    public class User
    {
        public const int PASSPORT_SERIES_LENGTH = 4;
        public const int PASSPORT_NUMBER_LENGTH = 6;
        public const int PHONE_NUMBER_LENGTH = 16;

        private User(Guid id, string name, string email, string passwordHash)
        {
            Id = id; 
            Name = name; 
            Email = email; 
            PasswordHash = passwordHash;
        }

        private User(Guid id, string name, string? surname, string? patronymic, string? passportNumber,
            string? passportSeries, DateOnly? birthDate, DateOnly registrationDate, string email, 
            string passwordHash, string? phoneNumber, bool isAdmin, List<BookIssuance> bookIssuances)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            PassportNumber = passportNumber;
            PassportSeries = passportSeries;
            BirthDate = birthDate;
            RegistrationDate = registrationDate;
            Email = email;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
            IsAdmin = isAdmin;
            BookIssuances = bookIssuances;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string? Surname { get; } = null;
        public string? Patronymic { get; } = null;
        public string? PassportNumber { get; } = null;
        public string? PassportSeries { get; } = null; 
        public DateOnly? BirthDate { get; } = null;
        public DateOnly RegistrationDate { get; } = DateOnly.FromDateTime(DateTime.Now);
        public string Email { get; }
        public string PasswordHash { get; }
        public string? PhoneNumber { get; } = null;
        public bool IsAdmin { get; } = false; 
        public List<BookIssuance> BookIssuances { get; } = new List<BookIssuance>();

        public static User Create(Guid id, string name, string? surname, string? patronymic, string? passportNumber,
            string? passportSeries, DateOnly? birthDate, DateOnly registrationDate, string email, string passwordHash,
            string? phoneNumber, bool isAdmin, List<BookIssuance> bookIssuances)
        { 
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name was null or empty");

            if(passportNumber != null && passportNumber.Length != PASSPORT_NUMBER_LENGTH)
                throw new ArgumentOutOfRangeException(nameof(passportNumber), passportNumber, $"invalid passportNumber length. expected: {PASSPORT_NUMBER_LENGTH}, actual: {passportNumber.Length}");

            if (passportSeries != null && passportSeries.Length != PASSPORT_SERIES_LENGTH)
                throw new ArgumentOutOfRangeException(nameof(passportSeries), passportSeries, $"invalid passportSeries length. expected: {PASSPORT_SERIES_LENGTH}, actual: {passportSeries.Length}");

            if (phoneNumber != null && phoneNumber.Length != PHONE_NUMBER_LENGTH)
                throw new ArgumentOutOfRangeException(nameof(phoneNumber), phoneNumber, $"invalid phoneNumber length. expected: {PHONE_NUMBER_LENGTH}, actual: {phoneNumber.Length}");

            return new User(
                id, name, surname, patronymic, passportNumber, passportSeries, 
                birthDate, registrationDate, email, passwordHash, phoneNumber, isAdmin, bookIssuances
            );
        }

        public static User Create(Guid id, string name, string email, string passwordHash)
        {
            return new User(id, name, email, passwordHash);
        }
    }
}

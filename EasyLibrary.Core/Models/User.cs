namespace EasyLibrary.Core.Models
{
    public class User
    {
        public const int PASSPORT_SERIES_LENGTH = 4;
        public const int PASSPORT_NUMBER_LENGTH = 6;
        public const int PHONE_NUMBER_LENGTH = 16;

        private User(Guid id, string name, string surname, string? patronymic, string passportNumber,
            string passportSeries, DateOnly birthDate, DateOnly registrationDate, string email, 
            string phoneNumber, bool isAdmin)
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
            PhoneNumber = phoneNumber;
            IsAdmin = isAdmin;
        }

        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public string Surname { get; } = string.Empty;
        public string? Patronymic { get; }
        public string PassportNumber { get; } = string.Empty;
        public string PassportSeries { get; } = string.Empty;
        public DateOnly BirthDate { get; }
        public DateOnly RegistrationDate { get; }
        public string Email { get; } = string.Empty;
        public string PhoneNumber { get; } = string.Empty;
        public bool IsAdmin { get; }

        public static User Create(Guid id, string name, string surname, string? patronymic, string passportNumber,
            string passportSeries, DateOnly birthDate, DateOnly registrationDate, string email,
            string phoneNumber, bool isAdmin)
        { 
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name was null or empty");

            if (string.IsNullOrEmpty(surname))
                throw new ArgumentException("surname was null or empty");

            if (passportNumber == null)
                throw new ArgumentNullException(nameof(passportNumber));
            else if(passportNumber.Length != PASSPORT_NUMBER_LENGTH)
                throw new ArgumentOutOfRangeException($"invalid passportNumber length. expected: {PASSPORT_NUMBER_LENGTH}, actual: {passportNumber.Length}");

            if (passportSeries == null)
                throw new ArgumentNullException(nameof(passportSeries));
            else if (passportSeries.Length != PASSPORT_SERIES_LENGTH)
                throw new ArgumentOutOfRangeException($"invalid passportSeries length. expected: {PASSPORT_SERIES_LENGTH}, actual: {passportSeries.Length}");

            if (phoneNumber == null)
                throw new ArgumentNullException(nameof(phoneNumber));
            else if (phoneNumber.Length != PHONE_NUMBER_LENGTH)
                throw new ArgumentOutOfRangeException($"invalid phoneNumber length. expected: {PHONE_NUMBER_LENGTH}, actual: {phoneNumber.Length}");
            

            return new User(
                id, name, surname, patronymic, passportNumber, passportSeries, 
                birthDate, registrationDate, email, phoneNumber, isAdmin
                );
        }
    }
}

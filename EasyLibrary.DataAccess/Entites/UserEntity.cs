namespace EasyLibrary.DataAccess.Entites
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? Patronymic { get; set; }
        public string PassportNumber { get; set; } = string.Empty;
        public string PassportSeries { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public DateOnly RegistrationDate { get; set; } = DateOnly.Parse(DateTime.Now.ToShortDateString());
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
    }
}

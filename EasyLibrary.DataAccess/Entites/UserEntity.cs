namespace EasyLibrary.DataAccess.Entites
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Surname { get; set; }
        public string? Patronymic { get; set; }
        public string? PassportNumber { get; set; }
        public string? PassportSeries { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateOnly RegistrationDate { get; set; } = DateOnly.Parse(DateTime.Now.ToShortDateString());
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public bool IsAdmin { get; set; } = false;
        public List<BookIssuanceEntity> BookIssuances { get; set; } = new List<BookIssuanceEntity>();
    }
}

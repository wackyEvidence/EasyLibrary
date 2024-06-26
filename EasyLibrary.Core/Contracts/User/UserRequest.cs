namespace EasyLibrary.Core.Contracts.User
{
    public record UserRequest(
        string Name,
        string Surname,
        string? Patronymic,
        string PassportNumber,
        string PassportSeries,
        DateOnly BirthDate,
        string Email,
        string PhoneNumber,
        bool IsAdmin
    );
}

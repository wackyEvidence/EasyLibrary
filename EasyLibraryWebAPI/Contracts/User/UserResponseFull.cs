namespace EasyLibrary.API.Contracts.User
{
    public record UserResponseFull(
        Guid Id,
        string Name,
        string Surname,
        string? Patronymic,
        string PassportNumber,
        string PassportSeries,
        DateOnly BirthDate,
        DateOnly RegistrationDate,
        string PhoneNumber,
        string Email,
        bool IsAdmin
    );
}

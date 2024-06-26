namespace EasyLibrary.Core.Contracts.User
{
    public record UserResponseDisplay(
        Guid Id,
        string Name,
        string? Surname,
        string? Patronymic,
        string Email,
        DateOnly RegistrationDate,
        bool IsAdmin
    );
}



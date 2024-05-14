namespace EasyLibrary.API.Contracts
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

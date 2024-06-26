namespace EasyLibrary.Core.Contracts.User
{
    public record RegisterUserRequest( 
        string Name,
        string Email, 
        string Password
    );
}

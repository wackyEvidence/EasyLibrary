namespace EasyLibrary.Core.Contracts.User
{
    public record LoginUserRequest( 
        string Email, 
        string Password
    );
}

namespace EasyLibrary.Core.Contracts.User
{
    public record UserRegistrationStatsResponseItem( 
        DateOnly Date, 
        int Count
    );
}

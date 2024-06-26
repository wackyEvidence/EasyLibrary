namespace EasyLibrary.Core.Contracts.BookIssuance
{
    public record BookIssuanceResponse(
        Guid Id, 
        Models.BookCopy BookCopy, 
        Models.User User, 
        DateOnly IssuanceDate, 
        bool IsFinished
    );
}

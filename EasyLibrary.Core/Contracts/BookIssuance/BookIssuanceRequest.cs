namespace EasyLibrary.Core.Contracts.BookIssuance
{
    public record BookIssuanceRequest(
        Guid BookCopyId, 
        Guid UserId, 
        DateOnly IssuanceDate, 
        bool IsFinished
    );
}

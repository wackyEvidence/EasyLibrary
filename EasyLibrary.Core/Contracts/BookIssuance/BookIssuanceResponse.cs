using EasyLibrary.Core.Models;

namespace EasyLibrary.Core.Contracts.BookIssuance
{
    public record BookIssuanceResponse(
        Guid Id, 
        Models.BookCopy BookCopy, 
        User User, 
        DateOnly IssuanceDate, 
        bool IsFinished
    );
}

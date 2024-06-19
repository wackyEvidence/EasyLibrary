using EasyLibrary.Core.Models;

namespace EasyLibrary.Core.Contracts.BookCopy
{
    public record BookCopyResponse( 
        Guid Id, 
        BookType BookType, 
        string InventoryNumber, 
        BookStatus Status
    );
}

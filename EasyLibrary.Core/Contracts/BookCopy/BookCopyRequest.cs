namespace EasyLibrary.Core.Contracts.BookCopy
{
    public record BookCopyRequest( 
        Guid TypeId, 
        string InventoryNumber, 
        int Status
    );
}

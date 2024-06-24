using EasyLibrary.Core;

namespace EasyLibrary.DataAccess.Entites
{
    public class BookCopyEntity
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public BookTypeEntity Type { get; set; } = null!;
        public string InventoryNumber { get; set; } = string.Empty;
        public BookStatus Status { get; set; }
        public List<BookIssuanceEntity> BookIssuances { get; set; } = new List<BookIssuanceEntity>();
    }
}

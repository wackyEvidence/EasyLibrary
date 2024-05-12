namespace EasyLibrary.Core.Models
{
    /// <summary>
    /// Конкретный экземпляр книги
    /// </summary>
    public class BookCopy
    {
        private BookCopy(Guid id, BookType type, string inventoryNumber, BookStatus status)
        {
            Id = id;
            Type = type;
            InventoryNumber = inventoryNumber;
            Status = status;
        }

        public Guid Id { get; }
        public BookType Type { get; }
        public string InventoryNumber { get; }
        public BookStatus Status { get; }

        public static BookCopy Create(Guid id, BookType type, string inventoryNumber, BookStatus status)
        {
            if(type == null) 
                throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrEmpty(inventoryNumber)) 
                throw new ArgumentException("inventoryNumber was null or empty"); 

            return new BookCopy(id, type, inventoryNumber, status);
        }
    }
}

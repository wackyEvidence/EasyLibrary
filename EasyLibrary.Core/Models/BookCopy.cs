using System.Text.RegularExpressions;

namespace EasyLibrary.Core.Models
{
    /// <summary>
    /// Конкретный экземпляр книги
    /// </summary>
    public class BookCopy
    {
        public const int INVENTORY_NUMBER_LENGTH = 10; 

        private BookCopy(Guid id, BookType type, string inventoryNumber, BookStatus status, List<BookIssuance> bookIssuances)
        {
            Id = id;
            Type = type;
            InventoryNumber = inventoryNumber;
            Status = status; 
            BookIssuances = bookIssuances;  
        }

        public Guid Id { get; }
        public BookType Type { get; }
        public string InventoryNumber { get; }
        public BookStatus Status { get; }
        public List<BookIssuance> BookIssuances { get; }

        public static BookCopy Create(Guid id, BookType type, string inventoryNumber, BookStatus status, List<BookIssuance> bookIssuances)
        {
            if(type == null) 
                throw new ArgumentNullException(nameof(type));

            if (string.IsNullOrEmpty(inventoryNumber))
                throw new ArgumentException("inventoryNumber was null or empty");
            else if (!Regex.IsMatch(inventoryNumber, $"^\\d{{{INVENTORY_NUMBER_LENGTH}}}$"))
                throw new ArgumentOutOfRangeException(nameof(inventoryNumber), inventoryNumber, "inventory number must be exactly 10 digits long");

            return new BookCopy(id, type, inventoryNumber, status, bookIssuances);
        }
    }
}

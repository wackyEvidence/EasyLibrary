namespace EasyLibrary.Core.Models
{
    /// <summary>
    /// Серия книги.
    /// </summary>
    public class BookSeries
    {
        private BookSeries(Guid id, string name, List<BookType> bookTypes)
        {
            Id = id;
            Name = name;
            BookTypes = bookTypes;
        }

        public Guid Id { get; }
        public string Name { get; }
        /// <summary>
        /// Книги данной серии.
        /// </summary>
        public List<BookType> BookTypes { get; }

        public static BookSeries Create(Guid id, string name, List<BookType> bookTypes)
        {
            if(string.IsNullOrEmpty(name)) 
                throw new ArgumentException("name was null or empty");

            return new BookSeries(id, name, bookTypes);
        }
    }
}

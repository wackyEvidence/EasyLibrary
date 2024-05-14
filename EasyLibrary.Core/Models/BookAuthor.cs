namespace EasyLibrary.Core.Models
{
    public class BookAuthor
    {
        private BookAuthor(Guid id, string name, string bio, List<BookType> bookTypes)
        {
            Id = id;
            Name = name;
            Bio = bio;
            BookTypes = bookTypes;
        }

        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public string? Bio { get; } 
        public List<BookType> BookTypes { get; }

        /// <summary>
        /// Создание экземпляра класса BookAuthor
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="name">Полное имя автора</param>
        /// <param name="bio">Краткая информация об авторе</param>
        /// <param name="bookTypes">Книги данного автора</param>
        /// <returns>Экземпляр класса BookAuthor</returns>
        /// <exception cref="ArgumentNullException">Полное имя автора - пустая строка или null</exception>
        public static BookAuthor Create(Guid id, string name, string bio, List<BookType> bookTypes)
        {
            if(string.IsNullOrEmpty(name)) 
                throw new ArgumentNullException("name can't be null or empty");
            
            return new BookAuthor(id, name, bio, bookTypes);
        }
    }
}

namespace EasyLibrary.Core.Models
{
    public class BookAuthor
    {
        public const int NAME_MAX_LENGTH = 100;
        public const int BIO_MAX_LENGTH = 300;

        private BookAuthor(Guid id, string name, string bio, List<BookType> bookTypes)
        {
            Id = id;
            Name = name;
            Bio = bio;
            BookTypes = bookTypes;
        }

        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        /// <summary>
        /// Краткое описание автора 
        /// </summary>
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
                throw new ArgumentNullException(nameof(name));
            else if(name.Length > NAME_MAX_LENGTH)
                throw new ArgumentOutOfRangeException($"name length was out of range. max allowed length: {NAME_MAX_LENGTH}, actual: {name.Length}");

            if (string.IsNullOrEmpty(bio))
                throw new ArgumentNullException(nameof(bio));
            else if (bio.Length > BIO_MAX_LENGTH)
                throw new ArgumentOutOfRangeException($"bio length was out of range. max allowed length: {BIO_MAX_LENGTH}, actual: {bio.Length}");

            return new BookAuthor(id, name, bio, bookTypes);
        }
    }
}

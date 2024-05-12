namespace EasyLibrary.Core.Models
{
    public class BookType
    {
        public Guid Id { get; }
        public string Title { get; } = string.Empty;
        public PublishingHouse PublishingHouse { get; }
        public BookSeries Series { get; }
        public List<BookCopy> Copies { get; }
        public List<BookAuthor> Authors { get; }
        public BookBinding Cover { get; }
        public int PublishingYear { get; }
        public string ISBN { get; } = string.Empty;
        public int PagesCount { get; }

        /// <summary>
        /// Вес книги в граммах. 
        /// </summary>
        public int Weight { get; }

        /// <summary>
        /// Доступна ли книги для выдачи читателю в личное пользование.
        /// </summary>
        public bool AvailableForIssuance { get; } 

        /// <summary>
        /// Сколько раз выдавалась книга - в личное пользование или для работы в библиотеке.
        /// </summary>
        public int TimesIssued { get; }

        /// <summary>
        /// Дата появления в ассортименте библиотеки.
        /// </summary>
        public DateOnly AppearanceDate { get; }

        /// <summary>
        /// Возрастное ограничение для читателей.
        /// </summary>
        public int MinAge { get; set; }
    }
}

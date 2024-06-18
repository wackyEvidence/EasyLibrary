namespace EasyLibrary.Core.Models
{
    public class BookType
    {
        public const int TITLE_MAX_LENGTH = 50; 
        public const int ISBN_LENGTH = 13;

        private BookType(Guid id, string title, PublishingHouse publishingHouse, BookSeries series, List<BookCopy> copies,
            List<BookAuthor> authors, BookBinding cover, int publishingYear, string isbn, int pagesCount, int weight, bool availableForIssuance, 
            int timesIssued, DateOnly appearanceDate, int minAge)
        {
            Id = id; 
            Title = title;
            PublishingHouse = publishingHouse;
            Series = series; 
            Copies = copies;
            Authors = authors;
            Cover = cover;
            PublishingYear = publishingYear;
            ISBN = isbn;
            PagesCount = pagesCount;
            Weight = weight;
            AvailableForIssuance = availableForIssuance;
            TimesIssued = timesIssued;
            AppearanceDate = appearanceDate;
            MinAge = minAge;
        }

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

        public static BookType Create(Guid id, string title, PublishingHouse publishingHouse, BookSeries series, List<BookCopy> copies,
            List<BookAuthor> authors, BookBinding cover, int publishingYear, string isbn, int pagesCount, int weight, bool availableForIssuance,
            int timesIssued, DateOnly appearanceDate, int minAge)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("title was null or empty");

            if (publishingHouse == null) 
                throw new ArgumentNullException(nameof(publishingHouse));

            if (series == null) 
                throw new ArgumentNullException(nameof(series));

            if (publishingYear < 0 || publishingYear > DateTime.Now.Year)
                throw new ArgumentOutOfRangeException($"publishingYear was out of range. expected range: 0 to {DateTime.Now.Year}, actual: {publishingYear}");

            if (string.IsNullOrEmpty(isbn))
                throw new ArgumentException("isbn was null or empty");
            else if (isbn.Length != ISBN_LENGTH)
                throw new ArgumentException($"invalid isnbn length. expected: {ISBN_LENGTH}, actual: {isbn.Length}");

            if (pagesCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(pagesCount), pagesCount, "pages count can't be less than or equal to 0");

            if (weight <= 0)
                throw new ArgumentOutOfRangeException(nameof(weight), weight, "weight can't be less than or equal to 0");

            if (minAge < 0 || minAge > 18)
                throw new ArgumentOutOfRangeException(nameof(minAge), minAge, "minAge can't be less than 0 or exceed 18");

            return new BookType(id, title, publishingHouse, series, copies, authors, cover, publishingYear, isbn, 
                pagesCount, weight, availableForIssuance, timesIssued, appearanceDate, minAge);
        }
    }
}

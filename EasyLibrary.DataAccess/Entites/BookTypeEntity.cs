using EasyLibrary.Core;

namespace EasyLibrary.DataAccess.Entites
{
    public class BookTypeEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int PublishingHouseId { get; set; }
        public PublishingHouseEntity PublishingHouse { get; set; } = null!;
        public int SeriesId { get; set; }
        public BookSeriesEntity Series { get; set; } = null!;
        public List<BookCopyEntity> Copies { get; set; } = new List<BookCopyEntity>();
        public List<BookAuthorEntity> Authors { get; set; } = new List<BookAuthorEntity>();
        /// <summary>
        /// Тип переплета книги.
        /// </summary>
        public BookBinding Binding { get; set; }
        public int PublishingYear { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public int PagesCount { get; set; }

        /// <summary>
        /// Вес книги в граммах.
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Доступна ли книги для выдачи читателю в личное пользование.
        /// </summary>
        public bool AvailableForIssuance { get; set; }

        /// <summary>
        /// Сколько раз выдавалась книга - в личное пользование или для работы в библиотеке.
        /// </summary>
        public int TimesIssued { get; set; }

        /// <summary>
        /// Дата появления в ассортименте библиотеки.
        /// </summary>
        public DateOnly AppearanceDate { get; set; }

        /// <summary>
        /// Возрастное ограничение для читателей.
        /// </summary>
        public int MinAge { get; set; }
    }
}

namespace EasyLibrary.Core.Models
{
    public class PublishingHouse
    {
        private PublishingHouse(Guid id, string name, List<BookType> bookTypes)
        {
            Id = id;
            Name = name;
            BookTypes = bookTypes;
        }

        public Guid Id { get; }
        public string Name { get; } = string.Empty;

        public List<BookType> BookTypes { get; }

        /// <summary>
        /// Создание экземпляра класса PublishingHouse
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="name">Наименование издательства</param>
        /// <param name="bookTypes">Книги данного издательства</param>
        /// <returns>Экземпляр класса Publishing House</returns>
        /// <exception cref="ArgumentException">Имя издательства - пустая строка или null</exception>
        public static PublishingHouse Create(Guid id, string name, List<BookType> bookTypes)  
        {
            if(string.IsNullOrEmpty(name)) 
                throw new ArgumentException("name can't be null or empty");
           
            return new PublishingHouse(id, name, bookTypes);
        }

        public static PublishingHouse Create(Guid id, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name can't be null or empty");

            return new PublishingHouse(id, name, new List<BookType>());
        }
    }
}

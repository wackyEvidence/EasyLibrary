namespace EasyLibrary.DataAccess.Entites
{
    public class BookSeriesEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Книги данной серии.
        /// </summary>
        public List<BookTypeEntity> BookTypes { get; set; } = new List<BookTypeEntity>();
    }
}

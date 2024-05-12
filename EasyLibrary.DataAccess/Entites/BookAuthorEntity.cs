namespace EasyLibrary.DataAccess.Entites
{
    public class BookAuthorEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; } 
        public List<BookTypeEntity> BookTypes { get; set; } = new List<BookTypeEntity>();
    }
}

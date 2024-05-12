namespace EasyLibrary.DataAccess.Entites
{
    public class PublishingHouseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<BookTypeEntity> BookTypes { get; set; } = new List<BookTypeEntity>();
    }
}

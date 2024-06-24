namespace EasyLibrary.DataAccess.Entites
{
    public class BookIssuanceEntity
    {
        public Guid Id { get; set; }
        public Guid BookCopyId { get; set; }
        public BookCopyEntity BookCopy { get; set; } = null!;
        public Guid UserId { get; set; }
        public UserEntity User { get; set; } = null!;
        public DateOnly IssuanceDate { get; set; }
        public bool IsFinished { get; set; }
    }
}

namespace EasyLibrary.Core.Models
{
    public class BookIssuance
    {
        private BookIssuance(
            Guid id, 
            BookCopy bookCopy, 
            User user, 
            DateOnly issuanceDate, 
            bool isFinished
            )
        {
            Id = id; 
            BookCopy = bookCopy;
            User = user;
            IssuanceDate = issuanceDate;
            IsFinished = isFinished;
        }

        public Guid Id { get; }
        public BookCopy BookCopy { get; }
        public User User { get; }
        public DateOnly IssuanceDate { get; }
        public bool IsFinished { get; }

        public static BookIssuance Create(Guid id, BookCopy bookCopy, User user, DateOnly issuanceDate, bool isFinished)
        {
            if (bookCopy == null)
                throw new ArgumentNullException(nameof(bookCopy));

            if(user == null) 
                throw new ArgumentNullException(nameof(user));

            if (issuanceDate == default)
                throw new ArgumentException("issuance date cannot be the default value", nameof(issuanceDate));

            if (issuanceDate > DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("issuance date is in the future!", nameof(issuanceDate));

            return new BookIssuance(id, bookCopy, user, issuanceDate, isFinished);
        }
    }
}

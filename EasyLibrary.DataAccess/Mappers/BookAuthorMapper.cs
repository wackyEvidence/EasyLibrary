using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;

namespace EasyLibrary.DataAccess.Mappers
{
    public class BookAuthorMapper : IMapper<BookAuthorEntity, BookAuthor>
    {
        private readonly Lazy<IMapper<BookTypeEntity, BookType>> _bookTypeMapper; 
        public BookAuthorMapper(Lazy<IMapper<BookTypeEntity, BookType>> bookTypeMapper)
        {
            _bookTypeMapper = bookTypeMapper;
        }

        public BookAuthor Map(BookAuthorEntity source)
        {
            return BookAuthor.Create(
                source.Id,
                source.Name,
                source.Bio,
                new List<BookType>()
            );
        }
    }
}

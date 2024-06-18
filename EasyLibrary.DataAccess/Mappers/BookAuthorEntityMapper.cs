using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;

namespace EasyLibrary.DataAccess.Mappers
{
    public class BookAuthorEntityMapper : IMapper<BookAuthor, BookAuthorEntity>
    {
        private readonly Lazy<IMapper<BookType, BookTypeEntity>> _bookTypeEntityMapper;

        public BookAuthorEntityMapper(Lazy<IMapper<BookType, BookTypeEntity>> bookTypeEntityMapper)
        {
            _bookTypeEntityMapper = bookTypeEntityMapper;
        }

        public BookAuthorEntity Map(BookAuthor source)
        {
            return new BookAuthorEntity
            {
                Id = source.Id,
                Name = source.Name,
                BookTypes = new List<BookTypeEntity>()
            };
        }
    }
}

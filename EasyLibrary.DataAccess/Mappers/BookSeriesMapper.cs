using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;

namespace EasyLibrary.DataAccess.Mappers
{
    public class BookSeriesMapper : IMapper<BookSeriesEntity, BookSeries>
    {
        private readonly Lazy<IMapper<BookTypeEntity, BookType>> _bookTypeMapper;

        public BookSeriesMapper(Lazy<IMapper<BookTypeEntity, BookType>> bookTypeMapper)
        {
            _bookTypeMapper = bookTypeMapper;   
        }

        public BookSeries Map(BookSeriesEntity source)
        {
            return BookSeries.Create(
                source.Id,
                source.Name,
                new List<BookType>()
            );
        }
    }
}

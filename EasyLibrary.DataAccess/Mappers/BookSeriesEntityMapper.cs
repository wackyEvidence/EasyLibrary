using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;

namespace EasyLibrary.DataAccess.Mappers
{
    public class BookSeriesEntityMapper : IMapper<BookSeries, BookSeriesEntity>
    {
        private readonly Lazy<IMapper<BookType, BookTypeEntity>> _bookTypeEntityMapper;

        public BookSeriesEntityMapper(Lazy<IMapper<BookType, BookTypeEntity>> bookTypeEntityMapper)
        {
            _bookTypeEntityMapper = bookTypeEntityMapper;
        }

        public BookSeriesEntity Map(BookSeries source)
        {
            return new BookSeriesEntity
            {
                Id = source.Id,
                Name = source.Name,
                BookTypes = source.BookTypes.ConvertAll(_bookTypeEntityMapper.Value.Map)
            };
        }
    }
}

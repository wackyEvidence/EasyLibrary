using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;

namespace EasyLibrary.DataAccess.Mappers
{
    public class BookTypeMapper : IMapper<BookTypeEntity, BookType>
    {
        private readonly Lazy<IMapper<PublishingHouseEntity, PublishingHouse>> _publishingHouseMapper;
        private readonly Lazy<IMapper<BookSeriesEntity, BookSeries>> _bookSeriesMapper;
        private readonly Lazy<IMapper<BookCopyEntity, BookCopy>> _bookCopyMapper;
        private readonly Lazy<IMapper<BookAuthorEntity, BookAuthor>> _bookAuthorMapper;

        public BookTypeMapper(
            Lazy<IMapper<PublishingHouseEntity, PublishingHouse>> publishingHouseMapper, 
            Lazy<IMapper<BookSeriesEntity, BookSeries>> bookSeriesMapper,
            Lazy<IMapper<BookCopyEntity, BookCopy>> bookCopyMapper, 
            Lazy<IMapper<BookAuthorEntity, BookAuthor>> bookAuthorMapper
            )
        {
            _publishingHouseMapper = publishingHouseMapper;
            _bookSeriesMapper = bookSeriesMapper;
            _bookCopyMapper = bookCopyMapper;
            _bookAuthorMapper = bookAuthorMapper;
        }

        public BookType Map(BookTypeEntity source)
        {
            return BookType.Create(
                source.Id, 
                source.Title, 
                _publishingHouseMapper.Value.Map(source.PublishingHouse), 
                _bookSeriesMapper.Value.Map(source.Series), 
                new List<BookCopy>(), 
                source.Authors.ConvertAll(_bookAuthorMapper.Value.Map),
                source.Cover,
                source.PublishingYear, 
                source.ISBN, 
                source.PagesCount, 
                source.Weight, 
                source.AvailableForIssuance, 
                source.TimesIssued, 
                source.AppearanceDate, 
                source.MinAge
            );
        }
    }
}

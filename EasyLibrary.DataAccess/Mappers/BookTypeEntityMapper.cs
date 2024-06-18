using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;

namespace EasyLibrary.DataAccess.Mappers
{
    public class BookTypeEntityMapper : IMapper<BookType, BookTypeEntity>
    {
        private readonly Lazy<IMapper<PublishingHouse, PublishingHouseEntity>> _publishingHouseEntityMapper; 
        private readonly Lazy<IMapper<BookSeries, BookSeriesEntity>> _bookSeriesEntityMapper; 
        private readonly Lazy<IMapper<BookCopy, BookCopyEntity>> _bookCopyEntityMapper;   
        private readonly Lazy<IMapper<BookAuthor, BookAuthorEntity>> _bookAuthorEntityMapper;


        public BookTypeEntityMapper(
            Lazy<IMapper<PublishingHouse, PublishingHouseEntity>> publishingHouseEntityMapper, 
            Lazy<IMapper<BookSeries, BookSeriesEntity>> bookSeriesEntityMapper, 
            Lazy<IMapper<BookCopy, BookCopyEntity>> bookCopyEntityMapper, 
            Lazy<IMapper<BookAuthor, BookAuthorEntity>> bookAuthorEntityMapper)
        {
            _publishingHouseEntityMapper = publishingHouseEntityMapper;
            _bookSeriesEntityMapper = bookSeriesEntityMapper;
            _bookCopyEntityMapper = bookCopyEntityMapper;
            _bookAuthorEntityMapper = bookAuthorEntityMapper;
        }

        public BookTypeEntity Map(BookType source)
        {
            return new BookTypeEntity()
            {
                Id = source.Id,
                Title = source.Title,
                PublishingHouse = _publishingHouseEntityMapper.Value.Map(source.PublishingHouse), 
                Series = _bookSeriesEntityMapper.Value.Map(source.Series),
                Copies = source.Copies.ConvertAll(_bookCopyEntityMapper.Value.Map), 
                Authors = source.Authors.ConvertAll(_bookAuthorEntityMapper.Value.Map),
                Cover = source.Cover, 
                PublishingYear = source.PublishingYear,
                ISBN = source.ISBN,
                PagesCount = source.PagesCount,
                Weight = source.Weight,
                AvailableForIssuance = source.AvailableForIssuance,
                TimesIssued = source.TimesIssued,
                AppearanceDate = source.AppearanceDate,
                MinAge = source.MinAge
            };
        }
    }
}

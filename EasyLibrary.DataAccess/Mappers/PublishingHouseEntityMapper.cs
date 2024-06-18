using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;

namespace EasyLibrary.DataAccess.Mappers
{
    public class PublishingHouseEntityMapper : IMapper<PublishingHouse, PublishingHouseEntity>
    {
        private readonly Lazy<IMapper<BookType, BookTypeEntity>> _bookTypeEntityMapper;

        public PublishingHouseEntityMapper(Lazy<IMapper<BookType, BookTypeEntity>> bookTypeEntityMapper)
        {
            _bookTypeEntityMapper = bookTypeEntityMapper;
        }

        public PublishingHouseEntity Map(PublishingHouse source)
        {
            return new PublishingHouseEntity()
            {
                Id = source.Id,
                Name = source.Name,
                BookTypes = source.BookTypes.ConvertAll(_bookTypeEntityMapper.Value.Map)
            };
        }
    }
}

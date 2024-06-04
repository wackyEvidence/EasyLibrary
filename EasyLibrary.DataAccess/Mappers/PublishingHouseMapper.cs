using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;

namespace EasyLibrary.DataAccess.Mappers
{
    public class PublishingHouseMapper : IMapper<PublishingHouseEntity, PublishingHouse>
    {
        private readonly Lazy<IMapper<BookTypeEntity, BookType>> _bookTypeMapper;

        public PublishingHouseMapper(Lazy<IMapper<BookTypeEntity, BookType>> bookTypeMapper)
        {
            _bookTypeMapper = bookTypeMapper;
        }

        public PublishingHouse Map(PublishingHouseEntity source)
        {
            return PublishingHouse.Create(
                source.Id, 
                source.Name, 
                source.BookTypes.ConvertAll(_bookTypeMapper.Value.Map)
            );
        }
    }
}

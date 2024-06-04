using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;

namespace EasyLibrary.DataAccess.Mappers
{
    public class BookCopyMapper : IMapper<BookCopyEntity, BookCopy>
    {
        private readonly Lazy<IMapper<BookTypeEntity, BookType>> _bookTypeMapper;

        public BookCopyMapper(Lazy<IMapper<BookTypeEntity, BookType>> bookTypeMapper)
        {
            _bookTypeMapper = bookTypeMapper;
        }

        public BookCopy Map(BookCopyEntity source)
        {
            return BookCopy.Create(
                source.Id, 
                _bookTypeMapper.Value.Map(source.Type), 
                source.InventoryNumber,
                source.Status
            );
        }
    }
}

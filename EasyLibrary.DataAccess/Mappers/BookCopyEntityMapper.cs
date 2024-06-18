using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;

namespace EasyLibrary.DataAccess.Mappers
{
    public class BookCopyEntityMapper : IMapper<BookCopy, BookCopyEntity>
    {
        private readonly Lazy<IMapper<BookType, BookTypeEntity>> _bookTypeEntityMapper;

        public BookCopyEntityMapper(Lazy<IMapper<BookType, BookTypeEntity>> bookTypeEntityMapper)
        {
            _bookTypeEntityMapper = bookTypeEntityMapper;
        }

        public BookCopyEntity Map(BookCopy source)
        {
            return new BookCopyEntity()
            {
                Id = source.Id,
                Type = _bookTypeEntityMapper.Value.Map(source.Type), 
                InventoryNumber = source.InventoryNumber,   
                Status = source.Status
            };
        }
    }
}

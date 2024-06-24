using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;

namespace EasyLibrary.DataAccess.Mappers
{
    public class BookIssuanceMapper : IMapper<BookIssuanceEntity, BookIssuance>
    {
        private readonly Lazy<IMapper<BookCopyEntity, BookCopy>> _bookCopyMapper;
        private readonly Lazy<IMapper<UserEntity, User>> _userMapper;

        public BookIssuanceMapper(
            Lazy<IMapper<BookCopyEntity, BookCopy>> bookCopyMapper, 
            Lazy<IMapper<UserEntity, User>> userMapper
            )
        {
            _bookCopyMapper = bookCopyMapper;
            _userMapper = userMapper;   
        }

        public BookIssuance Map(BookIssuanceEntity source)
        {
            return BookIssuance.Create(
                source.Id,
                _bookCopyMapper.Value.Map(source.BookCopy),
                _userMapper.Value.Map(source.User),
                source.IssuanceDate, 
                source.IsFinished
            );
        }
    }
}

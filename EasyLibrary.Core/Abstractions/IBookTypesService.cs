using EasyLibrary.Core.Models;
using EasyLibrary.DTO.BookTypes;

namespace EasyLibrary.Core.Abstractions
{   
    public interface IBookTypesService
    {
        Task<Guid> CreateBookType(BookTypeRequest request);
        Task<Guid> DeleteBookType(Guid id);
        Task<BookType> GetBookTypeById(Guid id);
        Task<List<BookType>> GetAllBookTypes();
        Task<Guid> UpdateBookType(Guid id, BookTypeRequest request);
    }
}

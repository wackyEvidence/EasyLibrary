using EasyLibrary.Core.Contracts.BookCopy;
using EasyLibrary.Core.Models;

namespace EasyLibrary.Core.Abstractions
{
    public interface IBookCopiesService
    {
        Task<Guid> CreateBookCopy(BookCopyRequest request);
        Task<Guid> DeleteBookCopy(Guid id);
        Task<BookCopy> GetBookCopyById(Guid id);
        Task<List<BookCopy>> GetAllBookCopies();
        Task<Guid> UpdateBookCopy(Guid id, BookCopyRequest request);
    }
}

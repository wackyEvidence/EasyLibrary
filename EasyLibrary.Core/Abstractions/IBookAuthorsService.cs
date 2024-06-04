using EasyLibrary.Core.Models;

namespace EasyLibrary.Core.Abstractions
{
    public interface IBookAuthorsService
    {
        Task<Guid> CreateBookAuthor(BookAuthor author);
        Task<Guid> DeleteBookAuthor(Guid id);
        Task<BookAuthor> GetBookAuthorById(Guid id);
        Task<List<BookAuthor>> GetAllBookAuthors();
        Task<Guid> UpdateBookAuthor(Guid id, string name, string? bio);
    }
}

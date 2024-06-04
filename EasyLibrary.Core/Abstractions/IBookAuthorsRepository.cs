using EasyLibrary.Core.Models;

namespace EasyLibrary.Core.Abstractions
{
    public interface IBookAuthorsRepository
    {
        Task<Guid> Create(BookAuthor author);
        Task<Guid> Delete(Guid id);
        Task<BookAuthor?> GetById(Guid id);
        Task<List<BookAuthor>> Get();
        Task<Guid> Update(Guid id, string name, string? bio);
    }
}

using EasyLibrary.Core.Models;

namespace EasyLibrary.Core.Abstractions
{
    public interface IBookTypesRepository
    {
        Task<Guid> Create(BookType bookType);
        Task<Guid> Delete(Guid id);
        Task<BookType?> GetById(Guid id);
        Task<List<BookType>> Get();
        Task<Guid> Update(BookType bookType);
    }
}

using EasyLibrary.Core.Models;

namespace EasyLibrary.Core.Abstractions
{
    public interface IBookIssuancesRepository
    {
        Task<Guid> Create(BookIssuance bookIssuance);
        Task<Guid> Delete(Guid id);
        Task<BookIssuance?> GetById(Guid id);
        Task<List<BookIssuance>> Get();
        Task<Guid> Update(BookIssuance bookIssuance);
    }
}

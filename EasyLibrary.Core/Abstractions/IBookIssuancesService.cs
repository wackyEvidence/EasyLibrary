using EasyLibrary.Core.Contracts.BookIssuance;
using EasyLibrary.Core.Models;

namespace EasyLibrary.Core.Abstractions
{
    public interface IBookIssuancesService
    {
        Task<Guid> CreateBookIssuance(BookIssuanceRequest request);
        Task<Guid> DeleteBookIssuance(Guid id);
        Task<BookIssuance> GetBookIssuanceById(Guid id);
        Task<List<BookIssuance>> GetAllBookIssuances();
        Task<Guid> UpdateBookIssuance(Guid id, BookIssuanceRequest request);
    }
}

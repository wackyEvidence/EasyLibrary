using EasyLibrary.Core.Models;

namespace EasyLibrary.Core.Abstractions
{
    public interface IBookSeriesRepository
    {
        Task<Guid> Create(BookSeries bookSeries);
        Task<Guid> Delete(Guid id);
        Task<BookSeries?> GetById(Guid id);
        Task<List<BookSeries>> Get();
        Task<Guid> Update(Guid id, string name);
    }
}

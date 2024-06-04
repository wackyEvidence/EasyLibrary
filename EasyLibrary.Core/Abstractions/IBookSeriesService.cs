using EasyLibrary.Core.Models;

namespace EasyLibrary.Core.Abstractions
{
    public interface IBookSeriesService
    {
        Task<Guid> CreateBookSeries(BookSeries bookSeries);
        Task<Guid> DeleteBookSeries(Guid id);
        Task<BookSeries> GetBookSeriesById(Guid id);
        Task<List<BookSeries>> GetAllBookSeries();
        Task<Guid> UpdateBookSeries(Guid id, string name);
    }
}

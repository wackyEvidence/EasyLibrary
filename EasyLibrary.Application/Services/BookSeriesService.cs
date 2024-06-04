using EasyLibrary.Application.Exceptions;
using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;

namespace EasyLibrary.Application.Services
{
    public class BookSeriesService : IBookSeriesService
    {
        private readonly IBookSeriesRepository _bookSeriesRepository;

        public BookSeriesService(IBookSeriesRepository bookSeriesRepository)
        {
            _bookSeriesRepository = bookSeriesRepository;
        }

        public async Task<Guid> CreateBookSeries(BookSeries bookSeries)
        {
            return await _bookSeriesRepository.Create(bookSeries);
        }

        public async Task<Guid> DeleteBookSeries(Guid id)
        {
            return await _bookSeriesRepository.Delete(id); 
        }

        public async Task<List<BookSeries>> GetAllBookSeries()
        {
            return await _bookSeriesRepository.Get();
        }

        public async Task<BookSeries> GetBookSeriesById(Guid id)
        {
            var bookSeries = await _bookSeriesRepository.GetById(id); 

            return bookSeries ?? throw new BookSeriesNotFoundException("Book series with provided id was not found");
        }

        public async Task<Guid> UpdateBookSeries(Guid id, string name)
        {
            return await _bookSeriesRepository.Update(id, name);
        }
    }
}

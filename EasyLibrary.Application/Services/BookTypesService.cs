using EasyLibrary.Application.Exceptions;
using EasyLibrary.Core;
using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DTO.BookTypes;

namespace EasyLibrary.Application.Services
{
    public class BookTypesService : IBookTypesService
    {
        private readonly IBookAuthorsRepository _bookAuthorsRepository;
        private readonly IBookSeriesRepository _bookSeriesRepository;
        private readonly IBookTypesRepository _bookTypesRepository;
        private readonly IPublishingHouseRepository _publishingHouseRepository;

        public BookTypesService(
            IBookAuthorsRepository bookAuthorsRepository,
            IBookSeriesRepository bookSeriesRepository, 
            IBookTypesRepository bookTypesRepository, 
            IPublishingHouseRepository publishingHouseRepository
           )
        {
            _bookAuthorsRepository = bookAuthorsRepository;
            _bookSeriesRepository = bookSeriesRepository;
            _bookTypesRepository = bookTypesRepository;
            _publishingHouseRepository = publishingHouseRepository;
        }

        public async Task<Guid> CreateBookType(BookTypeRequest request)
        {
            var bookType = await MapToBookType(request);

            return await _bookTypesRepository.Create(bookType); 
        }

        public async Task<Guid> DeleteBookType(Guid id)
        {
            return await _bookTypesRepository.Delete(id);
        }

        public async Task<List<BookType>> GetAllBookTypes()
        {
            return await _bookTypesRepository.Get();
        }

        public async Task<BookType> GetBookTypeById(Guid id)
        {
            var bookType = await _bookTypesRepository.GetById(id);

            return bookType ?? throw new NotFoundException<BookType>(id);
        }

        public async Task<Guid> UpdateBookType(Guid id, BookTypeRequest request)
        {
            var bookType = await MapToBookType(request, id);
            return await _bookTypesRepository.Update(bookType); 
        }

        private async Task<BookType> MapToBookType(BookTypeRequest request, Guid id = default)
        {
            var publishingHouse =
                await _publishingHouseRepository.GetById(request.PublishingHouseId) ??
                throw new NotFoundException<PublishingHouse>(request.PublishingHouseId);

            var bookSeries =
                await _bookSeriesRepository.GetById(request.BookSeriesId) ??
                throw new NotFoundException<BookSeries>(request.BookSeriesId);

            var authors = new List<BookAuthor>(); 

            foreach(var authorId in request.AuthorsId)
            {
                authors.Add(await _bookAuthorsRepository.GetById(authorId) ??
                    throw new NotFoundException<BookAuthor>(authorId));    
            }

            var bookType = BookType.Create(
                id = id == default? Guid.NewGuid() : id,
                request.Title,
                publishingHouse,
                bookSeries,
                new List<BookCopy>(),
                authors.ToList(),
                (BookBinding)request.Cover,
                request.PublishingYear,
                request.ISBN,
                request.PagesCount,
                request.Weight,
                request.AvailableForIssuance,
                0,
                DateOnly.Parse(DateTime.Now.ToShortDateString()),
                request.MinAge
            );

            return bookType;
        }
    }
}

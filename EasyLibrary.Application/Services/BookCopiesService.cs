using EasyLibrary.Application.Exceptions;
using EasyLibrary.Core;
using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Contracts.BookCopy;
using EasyLibrary.Core.Models;

namespace EasyLibrary.Application.Services
{
    public class BookCopiesService : IBookCopiesService
    {
        private readonly IBookCopiesRepository _bookCopiesRepository; 
        private readonly IBookTypesRepository _bookTypesRepository;

        public BookCopiesService(
            IBookCopiesRepository bookCopiesRepository, 
            IBookTypesRepository bookTypesRepository
        )
        {
            _bookCopiesRepository = bookCopiesRepository;   
            _bookTypesRepository = bookTypesRepository; 
        }

        public async Task<Guid> CreateBookCopy(BookCopyRequest request)
        {
            if (await _bookCopiesRepository.InventoryNumberExists(request.InventoryNumber))
                throw new ArgumentException("inventory number passed in request already exists", nameof(request.InventoryNumber));

            var bookCopy = await MapToBookCopy(request);
            return await _bookCopiesRepository.Create(bookCopy);
        }

        public async Task<Guid> DeleteBookCopy(Guid id)
        {
            return await _bookCopiesRepository.Delete(id);
        }

        public async Task<List<BookCopy>> GetAllBookCopies()
        {
            return await _bookCopiesRepository.Get();
        }

        public async Task<BookCopy> GetBookCopyById(Guid id)
        {
            var bookCopy = await _bookCopiesRepository.GetById(id);
            return bookCopy ?? throw new NotFoundException<BookCopy>(id);
        }

        public async Task<Guid> UpdateBookCopy(Guid id, BookCopyRequest request)
        {
            if (await _bookCopiesRepository.InventoryNumberExists(request.InventoryNumber, id))
                throw new ArgumentException("inventory number passed in request already exists", nameof(request.InventoryNumber));

            var bookCopy = await MapToBookCopy(request, id);
            return await _bookCopiesRepository.Update(bookCopy);
        }

        private async Task<BookCopy> MapToBookCopy(BookCopyRequest request, Guid id = default)
        {
            var bookType =
                await _bookTypesRepository.GetById(request.TypeId)
                ?? throw new NotFoundException<BookType>(request.TypeId);

            var bookCopy = BookCopy.Create(
                id = id == default ? Guid.Empty : id,
                bookType,
                request.InventoryNumber,
                (BookStatus)request.Status
            );

            return bookCopy;
        }
    }
}

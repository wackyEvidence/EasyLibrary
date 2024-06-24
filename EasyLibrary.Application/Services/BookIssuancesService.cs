using EasyLibrary.Application.Exceptions;
using EasyLibrary.Core;
using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Contracts.BookCopy;
using EasyLibrary.Core.Contracts.BookIssuance;
using EasyLibrary.Core.Models;

namespace EasyLibrary.Application.Services
{
    public class BookIssuancesService : IBookIssuancesService
    {
        private readonly IBookCopiesRepository _bookCopiesRepository;
        private readonly IBookIssuancesRepository _bookIssuancesRepository;
        private readonly IUsersRepository _usersRepository;

        public BookIssuancesService(IBookCopiesRepository bookCopiesRepository, IBookIssuancesRepository bookIssuancesRepository, IUsersRepository usersRepository)
        {
            _bookCopiesRepository = bookCopiesRepository;
            _bookIssuancesRepository = bookIssuancesRepository; 
            _usersRepository = usersRepository;
        }

        public async Task<Guid> CreateBookIssuance(BookIssuanceRequest request)
        {
            var bookIssuance = await MapToBookIssuance(request);
            return await _bookIssuancesRepository.Create(bookIssuance);
        }

        public async Task<Guid> DeleteBookIssuance(Guid id)
        {
            return await _bookIssuancesRepository.Delete(id);
        }

        public async Task<List<BookIssuance>> GetAllBookIssuances()
        {
            return await _bookIssuancesRepository.Get();
        }

        public async Task<BookIssuance> GetBookIssuanceById(Guid id)
        {
            var bookIssuance = await _bookIssuancesRepository.GetById(id);  
            return bookIssuance ?? throw new NotFoundException<BookIssuance>(id);
        }

        public async Task<Guid> UpdateBookIssuance(Guid id, BookIssuanceRequest request)
        {
            var bookIssuance = await MapToBookIssuance(request, id);
            return await _bookIssuancesRepository.Update(bookIssuance);
        }

        private async Task<BookIssuance> MapToBookIssuance(BookIssuanceRequest request, Guid id = default)
        {
            var bookCopy =
                await _bookCopiesRepository.GetById(request.BookCopyId)
                ?? throw new NotFoundException<BookCopy>(request.BookCopyId);   

            var user = 
                await _usersRepository.GetById(request.UserId) 
                ?? throw new NotFoundException<User>(request.UserId);

            var bookIssuance = BookIssuance.Create(
                id = id == default ? Guid.Empty : id,
                bookCopy,
                user,
                request.IssuanceDate,
                request.IsFinished
            );

            return bookIssuance;
        }
    }
}

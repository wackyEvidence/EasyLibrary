using EasyLibrary.Application.Exceptions;
using EasyLibrary.Core.Models;
using EasyLibrary.Core.Abstractions;

namespace EasyLibrary.Application.Services
{
    public class BookAuthorsService : IBookAuthorsService
    {
        private readonly IBookAuthorsRepository _bookAuthorsRepository;

        public BookAuthorsService(IBookAuthorsRepository bookAuthorsRepository)
        {
            _bookAuthorsRepository = bookAuthorsRepository;
        }

        public async Task<Guid> CreateBookAuthor(BookAuthor author)
        {
            return await _bookAuthorsRepository.Create(author);
        }

        public async Task<Guid> DeleteBookAuthor(Guid id)
        {
            return await _bookAuthorsRepository.Delete(id);
        }

        public async Task<List<BookAuthor>> GetAllBookAuthors()
        {
            return await _bookAuthorsRepository.Get();
        }

        public async Task<BookAuthor> GetBookAuthorById(Guid id)
        {
            var bookAuthor = await _bookAuthorsRepository.GetById(id);

            return bookAuthor ?? throw new BookAuthorNotFoundException("Book author with provided id was not found");
        }

        public async Task<Guid> UpdateBookAuthor(Guid id, string name, string? bio)
        {
            return await _bookAuthorsRepository.Update(id, name, bio);
        }
    }
}

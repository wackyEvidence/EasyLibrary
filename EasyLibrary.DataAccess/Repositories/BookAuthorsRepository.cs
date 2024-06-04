using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace EasyLibrary.DataAccess.Repositories
{
    public class BookAuthorsRepository : IBookAuthorsRepository
    {
        private readonly EasyLibraryDbContext _context;
        private readonly IMapper<BookAuthorEntity, BookAuthor> _bookAuthorMapper;

        public BookAuthorsRepository(EasyLibraryDbContext context, IMapper<BookAuthorEntity, BookAuthor> bookAuthorMapper)
        {
            _context = context;
            _bookAuthorMapper = bookAuthorMapper;
        }

        public async Task<Guid> Create(BookAuthor author)
        {
            var authorEntity = new BookAuthorEntity()
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio
            };

            await _context.BookAuthorEntity.AddAsync(authorEntity);
            await _context.SaveChangesAsync(); 

            return author.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.BookAuthorEntity
                .Where(ba => ba.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<BookAuthor>> Get()
        {
            var bookAuthorEntities = await _context.BookAuthorEntity.AsNoTracking().ToListAsync();

            return bookAuthorEntities.ConvertAll(_bookAuthorMapper.Map).ToList();
        }

        public async Task<BookAuthor?> GetById(Guid id)
        {
            var bookAuthor = await _context.BookAuthorEntity.Where(ba => ba.Id == id).FirstOrDefaultAsync();

            return bookAuthor == null ?
                null :
                _bookAuthorMapper.Map(bookAuthor);
        }

        public async Task<Guid> Update(Guid id, string name, string? bio)
        {
            await _context.BookAuthorEntity
                .Where(ba => ba.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(ba => ba.Name, name)
                    .SetProperty(ba => ba.Bio, bio)
                );
            return id;
        }
    }
}

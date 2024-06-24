using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace EasyLibrary.DataAccess.Repositories
{
    public class BookIssuancesRepository : IBookIssuancesRepository
    {
        private readonly EasyLibraryDbContext _context;
        private readonly IMapper<BookIssuanceEntity, BookIssuance> _bookIssuanceMapper;

        public BookIssuancesRepository(EasyLibraryDbContext context, IMapper<BookIssuanceEntity, BookIssuance> bookIssuanceMapper)
        {
            _context = context;
            _bookIssuanceMapper = bookIssuanceMapper;
        }

        public async Task<Guid> Create(BookIssuance bookIssuance)
        {
            var bookIssuanceEntity = new BookIssuanceEntity()
            {
                Id = bookIssuance.Id,
                BookCopyId = bookIssuance.BookCopy.Id,
                UserId = bookIssuance.User.Id,
                IssuanceDate = bookIssuance.IssuanceDate,   
                IsFinished = bookIssuance.IsFinished
            };

            await _context.BookIssuances.AddAsync(bookIssuanceEntity);  
            await _context.SaveChangesAsync();

            return bookIssuanceEntity.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.BookIssuances
                .Where(e => e.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<BookIssuance>> Get()
        {
            var bookIssuanceEntities = await _context.BookIssuances
                .Include(e => e.BookCopy)
                .Include(e => e.BookCopy.Type)
                .Include(e => e.BookCopy.Type.PublishingHouse)
                .Include(e => e.BookCopy.Type.Series)
                .Include(e => e.BookCopy.Type.Authors)
                .Include(e => e.User)
                .AsNoTracking()
                .ToListAsync();

            return bookIssuanceEntities.ConvertAll(_bookIssuanceMapper.Map);
        }

        public async Task<BookIssuance?> GetById(Guid id)
        {
            var bookIssuanceEntity = await _context.BookIssuances
                .AsNoTracking()
                .Where(e => e.Id == id)
                .Include(e => e.BookCopy)
                .Include(e => e.User)
                .FirstOrDefaultAsync();

            return bookIssuanceEntity == null? 
                null : 
                _bookIssuanceMapper.Map(bookIssuanceEntity);
        }

        public async Task<Guid> Update(BookIssuance bookIssuance)
        {
            await _context.BookIssuances
                .Where(e => e.Id == bookIssuance.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(s => s.BookCopyId, bookIssuance.BookCopy.Id)
                    .SetProperty(s => s.UserId, bookIssuance.User.Id)
                    .SetProperty(s => s.IssuanceDate, bookIssuance.IssuanceDate)
                    .SetProperty(s => s.IsFinished, bookIssuance.IsFinished)
                );
            return bookIssuance.Id;
        }
    }
}

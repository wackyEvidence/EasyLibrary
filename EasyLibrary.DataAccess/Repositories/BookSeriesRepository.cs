using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace EasyLibrary.DataAccess.Repositories
{
    public class BookSeriesRepository : IBookSeriesRepository
    {
        private readonly EasyLibraryDbContext _context;
        private readonly IMapper<BookSeriesEntity, BookSeries> _bookSeriesMapper;

        public BookSeriesRepository(EasyLibraryDbContext context, IMapper<BookSeriesEntity, BookSeries> bookSeriesMapper)
        {
            _context = context;
            _bookSeriesMapper = bookSeriesMapper;
        }

        public async Task<Guid> Create(BookSeries bookSeries)
        {
            var bookSeriesEntity = new BookSeriesEntity()
            {
                Id = bookSeries.Id,
                Name = bookSeries.Name
            };

            await _context.BookSeriesEntity.AddAsync(bookSeriesEntity);  
            await _context.SaveChangesAsync();

            return bookSeriesEntity.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.BookSeriesEntity
                .Where(bs => bs.Id == id)
                .ExecuteDeleteAsync();

            return id; 
        }

        public async Task<List<BookSeries>> Get()
        {
            var bookSeriesEntities = await _context.BookSeriesEntity.AsNoTracking().ToListAsync();

            return bookSeriesEntities.ConvertAll(_bookSeriesMapper.Map).ToList();
        }

        public async Task<BookSeries?> GetById(Guid id)
        {
            var bookSeriesEntity = await _context.BookSeriesEntity.AsNoTracking().Where(bs => bs.Id == id).FirstOrDefaultAsync();

            return bookSeriesEntity == null? 
                null : 
                _bookSeriesMapper.Map(bookSeriesEntity);
        }

        public async Task<Guid> Update(Guid id, string name)
        {
            await _context.BookSeriesEntity
                .Where(bs => bs.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(bs => bs.Name, name)
                );

            return id;
        }
    }
}

using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace EasyLibrary.DataAccess.Repositories
{
    public class BookTypesRepository : IBookTypesRepository
    {
        private readonly EasyLibraryDbContext _context;
        private readonly IMapper<PublishingHouseEntity, PublishingHouse> _publishingHouseMapper;
        private readonly IMapper<PublishingHouse, PublishingHouseEntity> _publishingHouseEntityMapper;
        private readonly IMapper<BookSeriesEntity, BookSeries> _bookSeriesMapper;
        private readonly IMapper<BookSeries, BookSeriesEntity> _bookSeriesEntityMapper;
        private readonly IMapper<BookAuthorEntity, BookAuthor> _bookAuthorsMapper;
        private readonly IMapper<BookAuthor, BookAuthorEntity> _bookAuthorsEntityMapper;
        private readonly IMapper<BookCopyEntity, BookCopy> _bookCopyMapper;
        private readonly IMapper<BookTypeEntity, BookType> _bookTypeMapper;

        public BookTypesRepository(
            EasyLibraryDbContext context,
            IMapper<PublishingHouseEntity, PublishingHouse> publishingHouseMapper,
            IMapper<PublishingHouse, PublishingHouseEntity> publishingHouseEntityMapper,
            IMapper<BookSeriesEntity, BookSeries> bookSeriesMapper,
            IMapper<BookSeries, BookSeriesEntity> bookSeriesEntityMapper,
            IMapper<BookAuthorEntity, BookAuthor> bookAuthorsMapper,
            IMapper<BookAuthor, BookAuthorEntity> bookAuthorsEntityMapper,
            IMapper<BookCopyEntity, BookCopy> bookCopyMapper,
            IMapper<BookTypeEntity, BookType> bookTypeMapper
            )
        {
            _context = context;
            _publishingHouseMapper = publishingHouseMapper;
            _publishingHouseEntityMapper = publishingHouseEntityMapper;
            _bookSeriesMapper = bookSeriesMapper;
            _bookSeriesEntityMapper = bookSeriesEntityMapper;
            _bookAuthorsMapper = bookAuthorsMapper;
            _bookAuthorsEntityMapper = bookAuthorsEntityMapper;
            _bookCopyMapper = bookCopyMapper;
            _bookTypeMapper = bookTypeMapper;
        }

        public async Task<Guid> Create(BookType bookType)
        {
            var bookTypeEntity = new BookTypeEntity()
            {
                Id = bookType.Id,
                Title = bookType.Title,
                PublishingHouseId = bookType.PublishingHouse.Id,
                SeriesId = bookType.Series.Id,
                Copies = new List<BookCopyEntity>(),
                Authors = bookType.Authors.ConvertAll(_bookAuthorsEntityMapper.Map),
                Cover = bookType.Cover, 
                PublishingYear = bookType.PublishingYear,
                ISBN = bookType.ISBN,
                PagesCount = bookType.PagesCount,
                Weight = bookType.Weight,
                AvailableForIssuance = bookType.AvailableForIssuance,   
                TimesIssued = bookType.TimesIssued, 
                AppearanceDate = DateOnly.Parse(DateTime.Now.ToShortDateString()),   
                MinAge = bookType.MinAge
            };

            foreach (var author in bookTypeEntity.Authors)
                _context.Entry(author).State = EntityState.Unchanged;
            

            await _context.BookTypes.AddAsync(bookTypeEntity);
            await _context.SaveChangesAsync();

            return bookType.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.BookTypes
                .Where(bt => bt.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<BookType>> Get()
        {
            var bookTypeEntities = await _context.BookTypes
                .Include(bt => bt.PublishingHouse)
                .Include(bt => bt.Series)
                .Include(bt => bt.Authors)
                .AsNoTracking()
                .ToListAsync();

            return bookTypeEntities.ConvertAll(_bookTypeMapper.Map);
        }

        public async Task<BookType?> GetById(Guid id)
        {
            var bookType = await _context.BookTypes
                .AsNoTracking()
                .Where(bt => bt.Id == id)
                .Include(bt => bt.Series)
                .Include(bt => bt.PublishingHouse) 
                .Include(bt => bt.Authors)
                .FirstOrDefaultAsync();
            
            return bookType == null ?
                null :
                BookType.Create(
                    bookType.Id, 
                    bookType.Title, 
                    _publishingHouseMapper.Map(bookType.PublishingHouse),
                    _bookSeriesMapper.Map(bookType.Series), 
                    bookType.Copies.ConvertAll(_bookCopyMapper.Map), 
                    bookType.Authors.ConvertAll(_bookAuthorsMapper.Map),
                    bookType.Cover, 
                    bookType.PublishingYear, 
                    bookType.ISBN, 
                    bookType.PagesCount, 
                    bookType.Weight, 
                    bookType.AvailableForIssuance, 
                    bookType.TimesIssued, 
                    bookType.AppearanceDate, 
                    bookType.MinAge
                );
        }

        public async Task<Guid> Update(BookType bookType)
        {
            var existingBookType = await _context.BookTypes
        .Include(bt => bt.Authors)
        .FirstOrDefaultAsync(bt => bt.Id == bookType.Id);

            if (existingBookType == null)
            {
                throw new KeyNotFoundException();
            }

            // Обновление простых свойств
            existingBookType.Title = bookType.Title;
            existingBookType.PublishingHouseId = bookType.PublishingHouse.Id;
            existingBookType.SeriesId = bookType.Series.Id;
            existingBookType.Cover = bookType.Cover;
            existingBookType.PublishingYear = bookType.PublishingYear;
            existingBookType.ISBN = bookType.ISBN;
            existingBookType.PagesCount = bookType.PagesCount;
            existingBookType.Weight = bookType.Weight;
            existingBookType.AvailableForIssuance = bookType.AvailableForIssuance;
            existingBookType.TimesIssued = bookType.TimesIssued;
            existingBookType.AppearanceDate = bookType.AppearanceDate;
            existingBookType.MinAge = bookType.MinAge;

            // Обновление связанных сущностей
            existingBookType.PublishingHouse = _publishingHouseEntityMapper.Map(bookType.PublishingHouse);
            _context.Entry(existingBookType.PublishingHouse).State = EntityState.Unchanged;

            existingBookType.Series = _bookSeriesEntityMapper.Map(bookType.Series);
            _context.Entry(existingBookType.Series).State = EntityState.Unchanged;

            // Обновление коллекции Authors
            existingBookType.Authors.Clear();
            foreach (var author in bookType.Authors)
            {
                var existingAuthorEntity = await _context.BookAuthorEntity.FindAsync(author.Id);
                if (existingAuthorEntity != null)
                {
                    _context.Entry(existingAuthorEntity).State = EntityState.Unchanged;
                    existingBookType.Authors.Add(existingAuthorEntity);
                }
                else
                {
                    var newAuthorEntity = _bookAuthorsEntityMapper.Map(author);
                    _context.BookAuthorEntity.Add(newAuthorEntity);
                    existingBookType.Authors.Add(newAuthorEntity);
                }
            }

            await _context.SaveChangesAsync();

            return bookType.Id;
        }
    }
}

using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace EasyLibrary.DataAccess.Repositories
{
    public class BookCopiesRepository : IBookCopiesRepository
    {
        private readonly EasyLibraryDbContext _context;
        private readonly IMapper<BookCopyEntity, BookCopy> _bookCopyMapper;
        private readonly IMapper<BookTypeEntity, BookType> _bookTypeMapper;  
        private readonly IMapper<BookType, BookTypeEntity> _bookTypeEntityMapper;  

        public BookCopiesRepository(
            EasyLibraryDbContext context, 
            IMapper<BookCopyEntity, BookCopy> bookCopyMapper,
            IMapper<BookTypeEntity, BookType> bookTypeMapper, 
            IMapper<BookType, BookTypeEntity> bookTypeEntityMapper)
        {
            _context = context; 
            _bookCopyMapper = bookCopyMapper;
            _bookTypeMapper = bookTypeMapper;   
            _bookTypeEntityMapper = bookTypeEntityMapper;   
        }

        /// <summary>
        /// Проверяет, существуют ли экземпляры книг с указанным инвентарным номером. 
        /// </summary>
        /// <param name="inventoryNumber">Инвентарный номер книги.</param>
        /// <param name="id">Id редактируемой книги. В случае создания имеет значение по умолчанию default.</param>
        /// <returns>
        /// В случае создания книги: true, если уже существуют книги с переданным инвентарным номером, в противном случае false. <br></br>
        /// В случае редактирования книги: true, если уже существуют книги (исключая редактируемую) с переданным инвентарным номером, в противном случае false. 
        /// </returns>
        public async Task<bool> InventoryNumberExists(string inventoryNumber, Guid id = default)
        {
            return await _context.BookCopies
                .AnyAsync(bc => 
                    bc.InventoryNumber == inventoryNumber && (id == default || bc.Id != id)
                );
        }

        public async Task<Guid> Create(BookCopy bookCopy)
        {
            var bookCopyEntity = new BookCopyEntity()
            {
                Id = bookCopy.Id,
                TypeId = bookCopy.Type.Id, 
                InventoryNumber = bookCopy.InventoryNumber,
                Status = bookCopy.Status
            };

            await _context.BookCopies.AddAsync(bookCopyEntity);
            await _context.SaveChangesAsync();

            return bookCopyEntity.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.BookCopies
                .Where(bc => bc.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<BookCopy>> Get()
        {
            var bookCopyEntities = await _context.BookCopies
                .Include(bc => bc.Type)
                .Include(bc => bc.Type.PublishingHouse)
                .Include(bc => bc.Type.Series)
                .Include(bc => bc.Type.Authors)
                .AsNoTracking()
                .ToListAsync();

            return bookCopyEntities.ConvertAll(_bookCopyMapper.Map);
        }

        public async Task<BookCopy?> GetById(Guid id)
        {
            var bookCopy = await _context.BookCopies
                .AsNoTracking()
                .Where(bc => bc.Id == id)
                .Include(bc => bc.Type)
                .FirstOrDefaultAsync();

            return bookCopy == null ?
                null :
                BookCopy.Create(
                    bookCopy.Id,
                    _bookTypeMapper.Map(bookCopy.Type),
                    bookCopy.InventoryNumber,
                    bookCopy.Status
                );
        }

        public async Task<Guid> Update(BookCopy bookCopy)
        {
            await _context.BookCopies
                .Where(bc => bc.Id == bookCopy.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(bc => bc.TypeId, bookCopy.Type.Id)
                    .SetProperty(bc => bc.InventoryNumber, bookCopy.InventoryNumber)
                    .SetProperty(bc => bc.Status, bookCopy.Status)  
                );
            return bookCopy.Id;
        }
    }
}

using EasyLibrary.Core.Models;

namespace EasyLibrary.Core.Abstractions
{
    public interface IBookCopiesRepository
    {
        Task<bool> InventoryNumberExists(string inventoryNumber,  Guid id = default);
        Task<Guid> Create(BookCopy bookCopy);
        Task<Guid> Delete(Guid id);
        Task<BookCopy?> GetById(Guid id);
        Task<List<BookCopy>> Get();
        Task<Guid> Update(BookCopy bookCopy);
    }
}

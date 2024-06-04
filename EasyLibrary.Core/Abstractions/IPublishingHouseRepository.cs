using EasyLibrary.Core.Models;

namespace EasyLibrary.Core.Abstractions
{
    public interface IPublishingHouseRepository
    {
        Task<Guid> Create(PublishingHouse publishingHouse);
        Task<Guid> Delete(Guid id);
        Task<PublishingHouse?> GetById(Guid id);
        Task<List<PublishingHouse>> Get();
        Task<Guid> Update(Guid id, string name);
    }
}

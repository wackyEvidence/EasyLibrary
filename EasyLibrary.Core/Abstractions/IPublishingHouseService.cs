using EasyLibrary.Core.Models;

namespace EasyLibrary.Core.Abstractions
{
    public interface IPublishingHouseService
    {
        Task<Guid> CreatePublishingHouse(PublishingHouse publishingHouse);
        Task<Guid> DeletePublishingHouse(Guid id);
        Task<PublishingHouse> GetPublishingHouseById(Guid id);
        Task<List<PublishingHouse>> GetAllPublishingHouses();
        Task<Guid> UpdatePublishingHouse(Guid id, string name);
    }
}

using EasyLibrary.Application.Exceptions;
using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;

namespace EasyLibrary.Application.Services
{
    public class PublishingHousesService : IPublishingHouseService
    {
        private readonly IPublishingHouseRepository _publishingHouseRepository;

        public PublishingHousesService(IPublishingHouseRepository publishingHouseRepository)
        {
            _publishingHouseRepository = publishingHouseRepository;
        }

        public async Task<Guid> CreatePublishingHouse(PublishingHouse publishingHouse)
        {
            return await _publishingHouseRepository.Create(publishingHouse); 
        }

        public async Task<Guid> DeletePublishingHouse(Guid id)
        {
            return await _publishingHouseRepository.Delete(id); 
        }

        public async Task<List<PublishingHouse>> GetAllPublishingHouses()
        {
            return await _publishingHouseRepository.Get();
        }

        public async Task<PublishingHouse> GetPublishingHouseById(Guid id)
        {
            var publishingHouse = await _publishingHouseRepository.GetById(id); 

            return publishingHouse ?? throw new PublishingHouseNotFoundException("Publishing house with provided id was not found");
        }

        public async Task<Guid> UpdatePublishingHouse(Guid id, string name)
        {
            return await _publishingHouseRepository.Update(id, name);
        }
    }
}

using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace EasyLibrary.DataAccess.Repositories
{
    public class PublishingHouseRepository : IPublishingHouseRepository
    {
        private readonly EasyLibraryDbContext _context;
        private readonly IMapper<PublishingHouseEntity, PublishingHouse> _publishingHouseMapper;

        public PublishingHouseRepository(EasyLibraryDbContext context, IMapper<PublishingHouseEntity, PublishingHouse> publishingHouseMapper)
        {
            _context = context; 
            _publishingHouseMapper = publishingHouseMapper; 
        }

        public async Task<Guid> Create(PublishingHouse publishingHouse)
        {
            var publishingHouseEntity = new PublishingHouseEntity()
            {
                Id = publishingHouse.Id,
                Name = publishingHouse.Name
            };

            await _context.PublishingHouseEntity.AddAsync(publishingHouseEntity); 
            await _context.SaveChangesAsync();  

            return publishingHouseEntity.Id;    
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.PublishingHouseEntity
                .Where(ph => ph.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<PublishingHouse>> Get()
        {
            var publishingHouseEntities = await _context.PublishingHouseEntity.AsNoTracking().ToListAsync();

            return publishingHouseEntities.ConvertAll(_publishingHouseMapper.Map).ToList(); 
        }

        public async Task<PublishingHouse?> GetById(Guid id)
        {
            var publishingHouseEntity = await _context.PublishingHouseEntity.AsNoTracking().Where(ph => ph.Id == id).FirstOrDefaultAsync();

            return publishingHouseEntity == null? 
                null : 
                _publishingHouseMapper.Map(publishingHouseEntity);  
        }

        public async Task<Guid> Update(Guid id, string name)
        {
            await _context.PublishingHouseEntity
                .Where(ph => ph.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(ph => ph.Name, name) 
                );

            return id; 
        }
    }
}

using EasyLibrary.API.Contracts.PublishingHouse;
using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishingHousesController : ControllerBase
    {
        private readonly IPublishingHouseService _publishingHouseService;
        public PublishingHousesController(IPublishingHouseService publishingHouseService)
        {
            _publishingHouseService = publishingHouseService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PublishingHouseResponse>> GetPublishingHouseById(Guid id)
        {
            try
            {
                var publishingHouse = await _publishingHouseService.GetPublishingHouseById(id);  

                var response = new PublishingHouseResponse(
                        publishingHouse.Id,
                        publishingHouse.Name
                    );

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<BookSeries>>> GetAllPublishingHouses()
        {
            var publishingHouses = await _publishingHouseService.GetAllPublishingHouses(); 
            return Ok(publishingHouses);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreatePublishingHouse([FromBody] PublishingHouseRequest request)
        {
            try
            {
                var publishingHouse = PublishingHouse.Create(
                    Guid.NewGuid(), 
                    request.Name, 
                    new List<BookType>());
                var publishingHouseId = await _publishingHouseService.CreatePublishingHouse(publishingHouse);

                return Ok(publishingHouseId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdatePublishingHouse(Guid id, [FromBody] PublishingHouseRequest request)
        {
            var publishingHouseId = await _publishingHouseService.UpdatePublishingHouse(id, request.Name);

            return Ok(publishingHouseId);
        }


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeletePublishingHouse(Guid id)
        {
            return Ok(await _publishingHouseService.DeletePublishingHouse(id));
        }
    }
}

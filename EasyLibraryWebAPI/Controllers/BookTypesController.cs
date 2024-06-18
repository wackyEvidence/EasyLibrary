using EasyLibrary.API.Contracts.User;
using EasyLibrary.Application.Services;
using EasyLibrary.Core.Abstractions;
using EasyLibrary.DTO.BookTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace EasyLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookTypesController : ControllerBase
    {
        private readonly IBookTypesService _bookTypesService;

        public BookTypesController(IBookTypesService bookTypesService)
        {
            _bookTypesService = bookTypesService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookTypeResponseFull>> GetBookTypeById(Guid id)
        {
            try
            {
                var bookType = await _bookTypesService.GetBookTypeById(id);

                var response = new BookTypeResponseFull(
                        bookType.Id, 
                        bookType.Title, 
                        bookType.Series, 
                        bookType.PublishingHouse, 
                        bookType.Authors,
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

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<BookTypeResponseDisplay>>> GetAllBookTypes()
        {
            var bookTypes = await _bookTypesService.GetAllBookTypes(); 

            if (Request.Headers.TryGetValue("Type", out StringValues typeHeader))
            {
                if (typeHeader == "full")
                {
                    var response = bookTypes.Select(bookType => new BookTypeResponseFull(
                        bookType.Id,
                        bookType.Title,
                        bookType.Series,
                        bookType.PublishingHouse,
                        bookType.Authors,
                        bookType.Cover,
                        bookType.PublishingYear,
                        bookType.ISBN,
                        bookType.PagesCount,
                        bookType.Weight,
                        bookType.AvailableForIssuance,
                        bookType.TimesIssued,
                        bookType.AppearanceDate,
                        bookType.MinAge
                    ));

                    return Ok(response);
                }
                else if (typeHeader == "display")
                {
                    var response = bookTypes.Select(bookType => new BookTypeResponseDisplay(
                        bookType.Id,
                        bookType.Title, 
                        bookType.Series,
                        bookType.PublishingHouse,
                        bookType.ISBN
                    ));

                    return Ok(response);
                }
                else
                    return BadRequest("Unexpected \"Type\" header value");
            }
            else
                return BadRequest("Expected \"Type\" header");
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBookType([FromBody] BookTypeRequest request)
        {
            try
            {
                var bookTypeId = await _bookTypesService.CreateBookType(request);
                return Ok(bookTypeId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBookType(Guid id, [FromBody] BookTypeRequest request)
        {
            try
            {
                var bookTypeId = await _bookTypesService.UpdateBookType(id, request);
                return Ok(bookTypeId);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBookType(Guid id)
        {
            return Ok(await _bookTypesService.DeleteBookType(id));
        }
    }
}

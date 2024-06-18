using EasyLibrary.API.Contracts.BookSeries;
using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookSeriesController : ControllerBase
    {
        private readonly IBookSeriesService _bookSeriesService;

        public BookSeriesController(IBookSeriesService bookSeriesService)
        {
            _bookSeriesService = bookSeriesService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookSeriesResponse>> GetBookSeriesById(Guid id)
        {
            try
            {
                var bookSeries = await _bookSeriesService.GetBookSeriesById(id);

                var response = new BookSeriesResponse(
                        bookSeries.Id,
                        bookSeries.Name
                    );

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<BookSeries>>> GetAllBookSeries()
        {
            var bookSeries = await _bookSeriesService.GetAllBookSeries();
            return Ok(bookSeries);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBookSeries([FromBody] BookSeriesRequest request)
        {
            try
            {
                var bookSeries = BookSeries.Create(
                    Guid.NewGuid(), 
                    request.Name, 
                    new List<BookType>());
                var bookSeriesId = await _bookSeriesService.CreateBookSeries(bookSeries);

                return Ok(bookSeriesId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBookSeries(Guid id, [FromBody] BookSeriesRequest request)
        {
            var bookSeriesId = await _bookSeriesService.UpdateBookSeries(id, request.Name);

            return Ok(bookSeriesId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBookSeries(Guid id)
        {
            return Ok(await _bookSeriesService.DeleteBookSeries(id));
        }
    }
}

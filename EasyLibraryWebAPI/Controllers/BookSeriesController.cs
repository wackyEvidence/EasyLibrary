using EasyLibrary.API.Contracts.BookAuthor;
using EasyLibrary.API.Contracts.BookSeries;
using EasyLibrary.Application.Services;
using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

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
                return NotFound(e.Message);
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
                var bookSeries = BookSeries.Create(Guid.NewGuid(), request.Name);
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

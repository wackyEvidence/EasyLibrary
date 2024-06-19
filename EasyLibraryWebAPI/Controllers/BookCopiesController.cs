using EasyLibrary.Application.Services;
using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Contracts.BookCopy;
using EasyLibrary.DTO.BookTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace EasyLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCopiesController : ControllerBase
    {
        private readonly IBookCopiesService _bookCopiesService;

        public BookCopiesController(IBookCopiesService bookCopiesService)
        {
            _bookCopiesService = bookCopiesService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookCopyResponse>> GetBookCopyById(Guid id)
        {
            try
            {
                var bookCopy = await _bookCopiesService.GetBookCopyById(id);

                var response = new BookCopyResponse(
                        bookCopy.Id, 
                        bookCopy.Type, 
                        bookCopy.InventoryNumber, 
                        bookCopy.Status
                );

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<BookCopyResponse>>> GetAllBookCopies()
        {
            var bookCopies = await _bookCopiesService.GetAllBookCopies();

            var response = bookCopies.Select(bookCopy => new BookCopyResponse(
                        bookCopy.Id,
                        bookCopy.Type,
                        bookCopy.InventoryNumber,
                        bookCopy.Status
            ));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBookCopy([FromBody] BookCopyRequest request)
        {
            try
            {
                var bookCopyId = await _bookCopiesService.CreateBookCopy(request);
                return Ok(bookCopyId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBookCopy(Guid id, [FromBody] BookCopyRequest request)
        {
            try
            {
                var bookCopyId = await _bookCopiesService.UpdateBookCopy(id, request);
                return Ok(bookCopyId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBookCopy(Guid id)
        {
            return Ok(await _bookCopiesService.DeleteBookCopy(id));
        }
    }
}

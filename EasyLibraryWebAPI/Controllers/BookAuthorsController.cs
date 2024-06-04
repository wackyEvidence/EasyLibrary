using EasyLibrary.API.Contracts.BookAuthor;
using EasyLibrary.API.Contracts.User;
using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace EasyLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAuthorsController : ControllerBase
    {
        private readonly IBookAuthorsService _bookAuthorsService;
        public BookAuthorsController(IBookAuthorsService bookAuthorsService) 
        { 
            _bookAuthorsService = bookAuthorsService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookAuthorResponseFull>> GetById(Guid id)
        {
            try
            {
                var bookAuthor = await _bookAuthorsService.GetBookAuthorById(id);

                var response = new BookAuthorResponseFull(
                        bookAuthor.Id,
                        bookAuthor.Name,
                        bookAuthor.Bio ?? string.Empty
                    );

                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookAuthors()
        {
            var bookAuthors = await _bookAuthorsService.GetAllBookAuthors();

            if (Request.Headers.TryGetValue("Type", out StringValues typeHeader))
            {
                if (typeHeader == "full")
                {
                    var response = bookAuthors.Select(ba => new BookAuthorResponseFull(
                        ba.Id,
                        ba.Name,
                        ba.Bio ?? string.Empty
                    ));

                    return Ok(response);
                }
                else if (typeHeader == "display")
                {
                    var response = bookAuthors.Select(ba => new BookAuthorResponseDisplay(
                        ba.Id,
                        ba.Name
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
        public async Task<ActionResult<Guid>> CreateBookAuthor([FromBody] BookAuthorRequest request)
        {
            try
            {
                var bookAuthor = BookAuthor.Create(Guid.NewGuid(), request.Name, request.Bio);
                var bookAuthorId = await _bookAuthorsService.CreateBookAuthor(bookAuthor);  

                return Ok(bookAuthorId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBookAuthor(Guid id, [FromBody] BookAuthorRequest request)
        {
            var bookAuthorId = await _bookAuthorsService.UpdateBookAuthor(
                id,
                request.Name,
                request.Bio
                );
       
            return Ok(bookAuthorId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBookAuthor(Guid id)
        {
            return Ok(await _bookAuthorsService.DeleteBookAuthor(id));
        }
    }
}

using EasyLibrary.Application.Services;
using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Contracts.BookIssuance;
using Microsoft.AspNetCore.Mvc;

namespace EasyLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookIssuancesController : ControllerBase
    {
        private readonly IBookIssuancesService _bookIssuanceService;

        public BookIssuancesController(IBookIssuancesService bookIssuanceService)
        {
            _bookIssuanceService = bookIssuanceService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookIssuanceResponse>> GetBookIssuanceById(Guid id)
        {
            try
            {
                var bookIssuance = await _bookIssuanceService.GetBookIssuanceById(id);

                var response = new BookIssuanceResponse(
                    bookIssuance.Id,
                    bookIssuance.BookCopy,
                    bookIssuance.User,
                    bookIssuance.IssuanceDate,
                    bookIssuance.IsFinished
                );

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<BookIssuanceResponse>>> GetAllBookIssuances()
        {
            var bookIssuances = await _bookIssuanceService.GetAllBookIssuances();

            var response = bookIssuances.Select(bookIssuance => new BookIssuanceResponse(
                    bookIssuance.Id,
                    bookIssuance.BookCopy,
                    bookIssuance.User,
                    bookIssuance.IssuanceDate,
                    bookIssuance.IsFinished
            ));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBookIssuance([FromBody] BookIssuanceRequest request)
        {
            try
            {
                var bookIssuanceId = await _bookIssuanceService.CreateBookIssuance(request);
                return Ok(bookIssuanceId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBookIssuance(Guid id, [FromBody] BookIssuanceRequest request)
        {
            try
            {
                var bookIssuanceId = await _bookIssuanceService.UpdateBookIssuance(id, request);
                return Ok(bookIssuanceId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBookIssuance(Guid id)
        {
            return Ok(await _bookIssuanceService.DeleteBookIssuance(id));
        }
    }
}

using Application.Commands.BookCopyCommand;
using Application.Queries.BookCopyQuery;
using Domain.Entities;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCopiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BookCopiesController> _logger;

        public BookCopiesController(IMediator mediator, ILogger<BookCopiesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<BookCopyEntity>> GetAllBookCopies()
        {
            return await _mediator.Send(new GetAllBookCopiesQuery());
        }

        [HttpGet("{barcode}")]
        public async Task<BookCopyEntity> GetBookCopyByBarcode(int barcode)
        {
            return await _mediator.Send(new GetBookCopyByBarcodeQuery { Barcode = barcode });
        }

        [HttpPost]
        public async Task<IActionResult> AddBookCopy([FromForm] CreateBookCopyCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetBookCopyByBarcode), new { barcode = command.Barcode }, result);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the book copy.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the book copy.");
            }
        }

        [HttpPut("{barcode}")]
        public async Task<IActionResult> UpdateBookCopy(int barcode, [FromForm] UpdateBookCopyCommand command)
        {
            if (barcode != command.Barcode)
            {
                return BadRequest("Barcode mismatch.");
            }

            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the book copy.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the book copy.");
            }
        }

        [HttpDelete("{barcode}/{bookId}")]
        public async Task<IActionResult> DeleteBookCopy(int barcode, int bookId)
        {
            try
            {
                var command = new DeleteBookCopyCommand { Barcode = barcode, BookId = bookId };
                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the book copy.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the book copy.");
            }
        }
    }
}

using Application.Commands.BookBorrow;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookBorrowController : ControllerBase
    {
        private readonly IBookBorrowService _bookBorrowService;
        private readonly IMediator _mediator;
        private readonly ILogger<BookBorrowController> _logger;

        public BookBorrowController(IBookBorrowService bookBorrowService, IMediator mediator,ILogger<BookBorrowController> logger)
        { 
            _bookBorrowService = bookBorrowService;
            _mediator = mediator;
           _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<BookBorrowEntity>> GetAllBookBorrow()
        {
            return await _mediator.Send(new GetAllBookBorrowQuery());
        }

        [HttpGet("{id}")]

        public async Task<BookBorrowEntity> GetBookBorrowById(int id)
        {
            return await _mediator.Send(new GetBookBorrowByIdQuery(id));
        }

        [HttpPost]

        public async Task<IActionResult> CreateBookBorrow([FromForm] BookBorrowDto bookBorrow)
        {
            try
            {
                var entity = new BookBorrowEntity
                {
                    UserId = bookBorrow.UserId,
                    BookId = bookBorrow.BookId,
                    BorrowDate = bookBorrow.BorrowDate,
                    ReturnDate = bookBorrow.ReturnDate,
                    Status = bookBorrow.Status,
                };

                var result = await _bookBorrowService.CreateBookBorrowAsync(entity);
                return CreatedAtAction(nameof(GetBookBorrowById), new { id = result.BorrowId }, result);


            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookBorrow(int id, [FromForm] BookBorrowDto bookBorrow)
        {
            if (id != bookBorrow.BorrowId)
            {
                return BadRequest("BorrowId not found");
            }
            try
            {
                var bookBorrowReturn = await _mediator.Send<int>(new UpdateBookBorrowCommand(
                    id,
                    bookBorrow.UserId,
                    bookBorrow.BookId,
                    bookBorrow.BorrowDate,
                    bookBorrow.ReturnDate,
                    bookBorrow.Status));

                return Ok(bookBorrowReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while updating the borrowed book");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while updating the book borrow");
            }
        }

        [HttpDelete("{id}")]
        public async Task<int> DeleteBookBorrow(int id)
        { 
            return await _mediator.Send(new DeleteBookBorrowCommand { Id = id });
        }
    }
}

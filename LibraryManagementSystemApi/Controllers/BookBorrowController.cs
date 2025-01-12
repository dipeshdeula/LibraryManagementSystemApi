using Application.Commands.BookBorrow;
using Application.DTOs;
using Application.Queries;
using Application.Queries.BookBorrowQuery;
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
                var command = new CreateBookBorrowCommand(
                    bookBorrow.UserId,
                  //  bookBorrow.BookId,
                    bookBorrow.Barcode,
                    bookBorrow.BorrowDate,
                    bookBorrow.ReturnDate,
                    bookBorrow.DueDate
                   // bookBorrow.Status
                    );

                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetBookBorrowById), new { id = result.BorrowId }, result);
                
         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the book borrow");
                return StatusCode(StatusCodes.Status500InternalServerError, new {Message = ex.Message });
                
            }
        }

        /*[HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookBorrow(int id, [FromForm] BookBorrowDto bookBorrow)
        {
            if (id != bookBorrow.BorrowId)
            {
                return BadRequest("BorrowId not found");
            }
            try
            {
                var command = new UpdateBookBorrowCommand(
                    id,
                    bookBorrow.UserId,                 
                    bookBorrow.Barcode,
                    bookBorrow.BorrowDate,
                    bookBorrow.ReturnDate
                  
                    );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while updating the borrowed book");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while updating the book borrow");
            }
        }*/

        [HttpDelete("{id}")]
        public async Task<int> DeleteBookBorrow(int id)
        { 
            return await _mediator.Send(new DeleteBookBorrowCommand { Id = id });
        }


        [HttpPost("return")]
        public async Task<IActionResult> ReturnBookBorrow([FromForm] ReturnBookBorrowDto returnBookBorrow)
        {
            try
            {
                var command = new ReturnBookBorrowCommand(
                    returnBookBorrow.UserId,
                    returnBookBorrow.Barcode,
                    returnBookBorrow.ReturnDate ?? DateTime.Now
                );

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning the book borrow");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
    }
}

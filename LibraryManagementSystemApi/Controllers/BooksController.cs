using Application.Commands;
using Application.Commands.BooksCommand;
using Application.DTOs;
using Application.Queries;
using Application.Queries.BooksQuery;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMediator _mediator;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService bookService, IMediator mediator, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<BooksEntity>> GetAllBooks()
        {
            return await _mediator.Send(new GetAllBooksQuery());
        }

        [HttpGet("{id}")]
        public async Task<BooksEntity> GetBookById(int id)
        {
            return await _mediator.Send(new GetBookByIdQuery(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromForm] BookDtos books)
        {
            try
            {
                if (books.AuthorId == 0)
                {
                    return BadRequest("AuthorId is Required");
                }
                //Validate and normalize the date format
              /*  if (!DateTime.TryParse(books.PublishedDate, out DateTime parseDate))
                {
                    return BadRequest("Invalid date format.Please use 'yyyy-MM-dd'.");
                }*/

                //Convert the normalized date to DateOnly
                
                //var publishDate = DateOnly.FromDateTime(parseDate);
               

                var createdBooks = await _mediator.Send(new CreateBookCommand(
                    books.Title,
                    books.AuthorId,
                    books.ISBN,
                    books.Genre,
                    books.Quantity,
                   // publishDate,
                    books.AvailabilityStatus));

                return Ok(
                    new
                    {
                        createdBooks.BookId,
                        createdBooks.Title,
                        createdBooks.AuthorId,
                        createdBooks.ISBN,
                        createdBooks.Genre,
                        createdBooks.Quantity,
                        //createdBooks.PublishedDate,
                        createdBooks.AvailabilityStatus



                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating the book");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured whilw creating the books");
            }
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateBook(int id, [FromForm] BookDtos books)
        {
            if (id != books.BookId)
            {
                return BadRequest("Book Id mismatch");
            }

            try
            {
                //validate and noramlize the date format
              /*  if(!DateTime.TryParse(books.PublishedDate, out DateTime parsedDate))
                {
                    return BadRequest("Invalid date format. Please use 'yyyy-MM-dd'.");
                }*/

                //Convert the normalized date to DateOnly
              //  var publishedDate = DateOnly.FromDateTime(parsedDate);

                var bookReturn = await _mediator.Send(new UpdateBookCommand(
                    id,
                    books.Title,
                    books.AuthorId,
                    books.Genre,
                    books.ISBN, 
                    books.Quantity,
                    //publishedDate,
                    books.AvailabilityStatus

                    ));

                return Ok(bookReturn);

            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while updating the book");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while updating the book");

            }
        }



        [HttpDelete("{id}")]
        public async Task<int> DeleteBook(int id)
        { 
            return await _mediator.Send(new DeleteBookCommand { Id = id});

        
        }
    }
}

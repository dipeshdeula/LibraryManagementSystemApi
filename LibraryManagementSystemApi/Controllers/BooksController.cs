using Application.Commands;
using Application.DTOs;
using Application.Queries;
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

                //convert DateOnlyDto to DateOnly
                // var PublishDate = new DateOnly(books.PublishDate.Year, books.PublishDate.Month, books.PublishDate.Day);

                var publishDate = books.PublishDate.ToDateOnly();
                
               

                var createdBooks = await _mediator.Send(new CreateBookCommand(
                    books.Title, books.AuthorId, books.ISBN, books.Genre, books.Quantity, books.PublishDate.ToDateOnly(), books.AvailabilityStatus));

                return Ok(
                    new
                    {
                        createdBooks.BookId,
                        createdBooks.Title,
                        createdBooks.AuthorId,
                        createdBooks.ISBN,
                        createdBooks.Genre,
                        createdBooks.Quantity,
                        PublishDate = createdBooks.PublishDate.ToString(),
                        createdBooks.AvailabilityStatus



                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating the book");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured whilw creating the books");
            }
        }
    }
}

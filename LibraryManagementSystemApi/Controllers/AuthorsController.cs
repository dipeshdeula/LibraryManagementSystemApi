using Application.Commands;
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
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMediator _mediator;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(IAuthorService authorService, IMediator mediator, ILogger<AuthorsController> logger)
        {
            _authorService = authorService;
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<AuthorsEntity>> GetAllAuthor()
        { 
            return await _mediator.Send(new GetAllAuthorsQuery());
        }

        [HttpGet("{id}")]
        public async Task<AuthorsEntity> GetAuthorById(int id)
        {
            return await _mediator.Send(new GetAuthorByIdQuery(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromForm] AuthorsDtos authors)
        {
            try
            {
                var createdAuthor = await _mediator.Send(new CreateAuthorCommand(
                    authors.AuthorName, authors.Biography, authors.AuthorProfile, authors.AuthorImage));

                //Generate the image URL
                var imageUrl = Url.Action("GetAuthorImage", "Authors", new { fileName = createdAuthor.AuthorProfile }, Request.Scheme);
                return Ok(
                    new
                    {
                        createdAuthor.AuthorId,
                        createdAuthor.AuthorName,
                        createdAuthor.Biography,
                        AuthorProfile = imageUrl

                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating the author");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the author.");
            }

        }

        [HttpGet("GetAuthorImage")]
        public IActionResult GetAuthorImage(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AuthorImages", fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var image = System.IO.File.OpenRead(filePath);
            return File(image, "image/jpeg");
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAuthor(int id, [FromForm] AuthorsDtos authors)
        {
            if (id != authors.AuthorId)
            {
                return BadRequest("Author ID mismatch.");
            }
            try
            {
               
                var authorReturn = await _mediator.Send(new UpdateAuthorCommand(
                    id,
                    authors.AuthorName,
                    authors.Biography,
                    authors.AuthorProfile,
                    authors.AuthorImage
                    ));

                return Ok(authorReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while updating the author");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurd while updating the author");
            }
        }

        [HttpDelete("{id}")]
        public async Task<int> DeleteAuthor(int id)
        {
            return await _mediator.Send(new DeleteAuthorCommand { Id = id });
        }
    }
}

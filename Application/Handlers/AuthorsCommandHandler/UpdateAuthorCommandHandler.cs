using Application.Commands.Author;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.AuthorsCommandHandler
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, int>
    {
        private readonly IAuthorService _authorService;

        public UpdateAuthorCommandHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<int> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorService.GetAuthorsByIdAsync(request.AuthorId);
            if (author == null)
            {
                throw new Exception("Author not found");
            }

            if (request.AuthorImage != null && request.AuthorImage.Length > 0)
            {
                // Generate a unique file name using Guid
                var fileName = Guid.NewGuid() + Path.GetExtension(request.AuthorImage.FileName);
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AuthorImages");

                // Ensure the directory exists
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Get the full file path
                var filePath = Path.Combine(directoryPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.AuthorImage.CopyToAsync(stream);
                }

                // Set the image URL in the author profile
                request.AuthorProfile = fileName;
            }

            /*  author.AuthorId = request.AuthorId;
              author.AuthorName = request.AuthorName;
              author.Biography = request.Biography;*/

            author.AuthorName = request.AuthorName ?? author.AuthorName;
            author.Biography = request.Biography ?? author.Biography;
            author.AuthorProfile = request.AuthorProfile ?? author.AuthorProfile;

            await _authorService.UpdateAuthorAsync(author, request.AuthorImage);
            return author.AuthorId;
        }
    }
}

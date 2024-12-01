using Application.Commands;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class UpdateAuthorCommandHandler:IRequestHandler<UpdateAuthorCommand, int>
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
                //Generate a unique file name using Guid
                var fileName = Guid.NewGuid() + Path.GetExtension(request.AuthorImage.FileName);
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AuthorImages");

                //check if the directory and file name to get the full file path
                var filePath = Path.Combine(directoryPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.AuthorImage.CopyToAsync(stream);
                }

                //set the image URL in the author profile
                request.AuthorProfile = fileName;
            }

            author.AuthorId = request.AuthorId;
            author.AuthorName = request.AuthorName;
            author.Biography = request.Biography;

            await _authorService.UpdateAuthorAsync(author);
            return author.AuthorId;
        }
    }
}

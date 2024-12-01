using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class CreateAurthorCommandHandler : IRequestHandler<CreateAuthorCommand, AuthorsEntity>
    {
        private readonly IAuthorService _authorService;

        public CreateAurthorCommandHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public async Task<AuthorsEntity> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            if (request.AuthorImage != null && request.AuthorImage.Length > 0)
            {
                //Generate a unique file name using Guid
                var fileName = Guid.NewGuid() + Path.GetExtension(request.AuthorImage.FileName);
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AuthorImages");

                //check if the directory exists, if not, create it
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                //Combine the directory and file name to get the full file path
                var filePath = Path.Combine(directoryPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.AuthorImage.CopyToAsync(stream);
                }

                //set the image url in the author profile
                request.AuthorProfile = fileName;
            }

            var author = new AuthorsEntity
            {
                AuthorName = request.AuthorName,
                Biography = request.Biography,
                AuthorProfile = request.AuthorProfile,

            };

            return await _authorService.CreateAuthorAsync(author);
        }
    }
}

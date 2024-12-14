using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<IEnumerable<AuthorsEntity>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAuthorsAsync();
        }

        public async Task<AuthorsEntity> GetAuthorsByIdAsync(int id)
        {
            return await _authorRepository.GetAuthorsByIdAsync(id);
        }
        public async Task<AuthorsEntity> CreateAuthorAsync(AuthorsEntity author)
        {
             await _authorRepository.AddAuthorAsync(author);
            return author;
        }

        public async Task UpdateAuthorAsync(AuthorsEntity author, IFormFile? authorImage =null)
        {
            var existingAuthor = await _authorRepository.GetAuthorsByIdAsync(author.AuthorId);
            if (existingAuthor == null)
            {
                throw new Exception("Author not found");
            }

            existingAuthor.AuthorName = author.AuthorName ?? existingAuthor.AuthorName;
            existingAuthor.Biography = author.Biography ?? existingAuthor.Biography;
            existingAuthor.AuthorProfile = author.AuthorProfile ?? existingAuthor.AuthorProfile;

            //Handle the AuthorImage property
           if (authorImage != null)
            { 
                //Save the image to a file or database and update the AuthorProfile property
                var imagePath = SaveAuthorImage(authorImage);
                existingAuthor.AuthorProfile = imagePath;
            }

            await _authorRepository.UpdateAuthorAsync(author);

        }

        public async Task DeleteAuthorAsync(int id)
        {
            await _authorRepository.DeleteAuthorAsync(id);
        }

        private string SaveAuthorImage(IFormFile authorImage)
        {
            // Implement the logic to save the image and return the file path or URL
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AuthorImages", authorImage.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                authorImage.CopyTo(stream);
            }
            return filePath;
        }
    }
}

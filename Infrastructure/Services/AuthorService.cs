using Domain.Entities;
using Domain.Interfaces;

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

        public async Task UpdateAuthorAsync(AuthorsEntity author)
        {
            await _authorRepository.UpdateAuthorAsync(author);

        }

        public async Task DeleteAuthorAsync(int id)
        {
            await _authorRepository.DeleteAuthorAsync(id);
        }

      
    }
}

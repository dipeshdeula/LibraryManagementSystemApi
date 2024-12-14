using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorsEntity>> GetAllAuthorsAsync();
        Task<AuthorsEntity> GetAuthorsByIdAsync(int id);
        Task<AuthorsEntity> CreateAuthorAsync(AuthorsEntity author);
        Task UpdateAuthorAsync(AuthorsEntity author, IFormFile? authorImage=null);
        Task DeleteAuthorAsync(int id);

    }
}

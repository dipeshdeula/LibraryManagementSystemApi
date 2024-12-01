using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<AuthorsEntity>> GetAllAuthorsAsync();
        Task<AuthorsEntity> GetAuthorsByIdAsync(int id);
        Task<string> AddAuthorAsync(AuthorsEntity author);
        Task<string>UpdateAuthorAsync(AuthorsEntity author);
        Task<string> DeleteAuthorAsync(int id);

    }
}

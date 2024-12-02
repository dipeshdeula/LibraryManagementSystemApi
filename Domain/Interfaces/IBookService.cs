using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
   public interface IBookService
    {
        Task<IEnumerable<BooksEntity>> GetAllBooksAsync();
        Task<BooksEntity> GetBooksByIdAsync(int id);
        Task<BooksEntity> CreateBooksAsync(BooksEntity books);
        Task UpdateBooksAsync(BooksEntity books);
        Task DeleteBooksAsync(int id);

    }
}

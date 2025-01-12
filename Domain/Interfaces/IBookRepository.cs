using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public  interface IBookRepository
    {
        Task<IEnumerable<BooksEntity>> GetAllBooksAsync();
        Task<BooksEntity> GetBooksByIdAsync(int id);
        Task<string> AddBooksAsync(BooksEntity books);
        Task<string> UpdateBooksAsync(BooksEntity books);
        Task<string> DeleteBooksAsync(int id);
       
    }
}

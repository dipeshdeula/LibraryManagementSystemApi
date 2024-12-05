using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookBorrowService
    {
        Task<IEnumerable<BookBorrowEntity>> GetAllBookBorrowAsync();
        Task<BookBorrowEntity> GetBookBorrowByIdAsync(int id);
        Task<BookBorrowEntity> CreateBookBorrowAsync(BookBorrowEntity bookBorrow);
        Task UpdateBookBorrowAsync(BookBorrowEntity bookBorrow);
        Task DeleteBookBorrowAsync(int id);
    }
}

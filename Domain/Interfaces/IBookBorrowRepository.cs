using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookBorrowRepository
    {
        Task<IEnumerable<BookBorrowEntity>> GetAllBookBorrowAsync();
        Task<BookBorrowEntity> GetBookBorrowAsync(int id);
        Task<string> AddBookBorrowAsync(BookBorrowEntity bookBorrow);
        Task<string> UpdateBookBorrowAsync(BookBorrowEntity bookBorrow);
        Task<string> DeleteBookBorrowAsync(int id);
        Task<bool> IsBookCopyAvailable(int barcode);
        Task<string> ReturnBookBorrowAsync(int userId, int barcode, DateTime? returnDate);
        Task<string> MarkOverdueBooksAsync();
    }
}

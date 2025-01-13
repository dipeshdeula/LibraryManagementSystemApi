using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookCopyRepository
    {
        Task<IEnumerable<BookCopyEntity>> GetAllBookCopiesAsync();
        Task<BookCopyEntity> GetBookCopyByBarcodeAsync(int barcode);
        Task<string> AddBookCopyAsync(BookCopyEntity bookCopy);
        Task<string> UpdateBookCopyAsync(BookCopyEntity bookCopy);
        Task<string> DeleteBookCopyAsync(int barcode, int bookId);
    }
}

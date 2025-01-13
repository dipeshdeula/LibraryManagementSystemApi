using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IBookCopyService
    {
        Task<IEnumerable<BookCopyEntity>> GetAllBookCopiesAsync();
        Task<BookCopyEntity> GetBookCopyByBarcodeAsync(int barcode);
        Task<string> AddBookCopyAsync(BookCopyEntity bookCopy);
        Task<BookCopyEntity> UpdateBookCopyAsync(BookCopyEntity bookCopy);
        Task DeleteBookCopyAsync(int barcode, int bookId);

    }
}

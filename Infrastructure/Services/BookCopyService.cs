using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class BookCopyService : IBookCopyService
    {
        private readonly IBookCopyRepository _bookCopyRepository;

        public BookCopyService(IBookCopyRepository bookCopyRepository)
        {
            _bookCopyRepository = bookCopyRepository;
        }

        public async Task<IEnumerable<BookCopyEntity>> GetAllBookCopiesAsync()
        {
            return await _bookCopyRepository.GetAllBookCopiesAsync();
        }

        public async Task<BookCopyEntity> GetBookCopyByBarcodeAsync(int barcode)
        {
            return await _bookCopyRepository.GetBookCopyByBarcodeAsync(barcode);
        }

        public async Task<string> AddBookCopyAsync(BookCopyEntity bookCopy)
        {
            try
            {
                var response = await _bookCopyRepository.AddBookCopyAsync(bookCopy);
                if (response.Contains("failed", System.StringComparison.OrdinalIgnoreCase))
                    throw new System.InvalidOperationException(response);

                return response;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in AddBookCopyAsync:{ex.Message}");
                throw;
            }
        }

        public async Task<BookCopyEntity> UpdateBookCopyAsync(BookCopyEntity bookCopy)
        {
            var response = await _bookCopyRepository.UpdateBookCopyAsync(bookCopy);
            if (response.Contains("failed", System.StringComparison.OrdinalIgnoreCase))
                throw new System.InvalidOperationException(response);

            return bookCopy;
        }

        public async Task DeleteBookCopyAsync(int barcode, int bookId)
        {
            var response = await _bookCopyRepository.DeleteBookCopyAsync(barcode, bookId);
            if (response.Contains("failed", System.StringComparison.OrdinalIgnoreCase))
                throw new System.InvalidOperationException(response);
        }

    }
}

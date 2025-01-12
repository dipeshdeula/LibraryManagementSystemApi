using Azure;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class BookBorrowService : IBookBorrowService
    {
        private int _borrowPeriodDays = 90;
        private readonly IBookBorrowRepository _bookBorrowRepository;

        public BookBorrowService(IBookBorrowRepository bookBorrowRepository)
        {
            _bookBorrowRepository = bookBorrowRepository;
        }

        public async Task<IEnumerable<BookBorrowEntity>> GetAllBookBorrowAsync()
        {
            return await _bookBorrowRepository.GetAllBookBorrowAsync();
        }

        public async Task<BookBorrowEntity> GetBookBorrowByIdAsync(int id)
        {
            return await _bookBorrowRepository.GetBookBorrowAsync(id);
        }

        public async Task<BookBorrowEntity> CreateBookBorrowAsync(BookBorrowEntity bookBorrow)
        {
            //validate the borrow data
            if (bookBorrow.BorrowDate > DateTime.Now)
                throw new ArgumentException("Borrow date cannot be in the future");

            //check book availability
            var isAvailable = await _bookBorrowRepository.IsBookCopyAvailable(bookBorrow.Barcode);
            if (!isAvailable)
                throw new InvalidOperationException("This book copy is not available for borrowing.");

            //fetch existing borrow records
            var existingBorrows = await _bookBorrowRepository.GetAllBookBorrowAsync();

            //check if the book is already borrowed and not returned
            bool isBookBorrowed = existingBorrows.Any(b => b.Barcode == bookBorrow.Barcode && (b.Status == "Borrowed" || b.Status == "Overdue"));

            if (isBookBorrowed)
                throw new InvalidOperationException("The book is already borrowed by another user.");

            //Ensure the same user doesn't borrow the same book again without returning it
            bool isBookBorrowedBySameUser = existingBorrows.Any(b =>
                b.Barcode == bookBorrow.Barcode &&
                b.UserId == bookBorrow.UserId &&
                b.Status == "Borrowed");

            if (isBookBorrowedBySameUser)
                throw new InvalidOperationException("You cannot borrow the same book again until it is returned.");

            bookBorrow.DueDate = bookBorrow.BorrowDate.AddDays(_borrowPeriodDays);
            bookBorrow.Status = "Borrowed";

            //call repository to add the borrow record
            var response = await _bookBorrowRepository.AddBookBorrowAsync(bookBorrow);

            //check repository response
            if (response.Contains("failed", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException(response);

            return bookBorrow;
        }

        public async Task UpdateBookBorrowAsync(BookBorrowEntity bookBorrow)
        {
            //validate the update operation
            if (bookBorrow.BorrowId <= 0)
                throw new ArgumentException("Invalid Borrow ID.");

            //check if the return date is valid (must be after borrow date)
            if (bookBorrow.ReturnDate.HasValue && bookBorrow.ReturnDate <= bookBorrow.BorrowDate)
                throw new ArgumentException("Return date must be after the borrow date.");

            //Automatically update the status based on the presence of a return date
            bookBorrow.Status = bookBorrow.ReturnDate.HasValue ? "Returned" : "Borrowed";

            //update the record via the repository
            var response = await _bookBorrowRepository.UpdateBookBorrowAsync(bookBorrow);

            if (response.Contains("failed"))
                throw new InvalidOperationException("failed to update the book borrow record.");
        }

        public async Task DeleteBookBorrowAsync(int id)
        {
            await _bookBorrowRepository.DeleteBookBorrowAsync(id);
        }

        public async Task<BookBorrowEntity> ReturnBookBorrowAsync(int userId, int barcode, DateTime? returnDate)
        {
            //validate the return date
            if (returnDate.HasValue && returnDate > DateTime.Now)
                throw new ArgumentException("Return date cannot be in the future.");

            //call repository to update the borrow record
            var response = await _bookBorrowRepository.ReturnBookBorrowAsync(userId, barcode, returnDate);

            //check repository response
            if (response == null || response.Contains("failed", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException(response ?? "Operation failed");


            // Assuming you need to fetch the updated BookBorrowEntity after returning the book
            var updatedBookBorrow = await _bookBorrowRepository.GetBookBorrowAsync(userId);
            if (updatedBookBorrow == null)
                throw new InvalidOperationException("Failed to retrieve the updated book borrow record.");

            return updatedBookBorrow;
        }

        public async Task<string> MarkOverdueBooksAsync()
        {
            return await _bookBorrowRepository.MarkOverdueBooksAsync();
        }
    }
}

using Azure;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class BookBorrowService : IBookBorrowService
    {
        private readonly IBookBorrowRepository _bookBorrowRepository;

        public BookBorrowService(IBookBorrowRepository bookBorrowRepository)
        { 
            _bookBorrowRepository = bookBorrowRepository;
        }

        public async Task<IEnumerable<BookBorrowEntity>> GetAllBookBorrowAsync()
        {
            return await _bookBorrowRepository.GetAllBookBorrowAsync();
        }

        public async Task <BookBorrowEntity> GetBookBorrowByIdAsync(int id)
        {
            return await _bookBorrowRepository.GetBookBorrowAsync(id);
            
        }


        public async Task<BookBorrowEntity> CreateBookBorrowAsync(BookBorrowEntity bookBorrow)
        {
            //validate the borrow data
            if (bookBorrow.BorrowDate > DateTime.Now)
                throw new ArgumentException("Borrow date cannot be in the future");

            //fetch existing borrow records
            var existingBorrows = await _bookBorrowRepository.GetAllBookBorrowAsync();

            //check if the book is already borrowed and not returned
            bool isBookBorrowed = existingBorrows.Any(b => b.BookId == bookBorrow.BookId && b.Status == "Borrowed");

            if (isBookBorrowed)
                throw new InvalidOperationException("The book is already borrowed by another user.");


            //Ensure the same user doesn't borrow the same book again without returning it
            bool isBookBorrowedBySameUser = existingBorrows.Any(b =>
                b.BookId == bookBorrow.BookId &&
                b.UserId == bookBorrow.UserId &&
                b.Status == "Borrowed");

            if (isBookBorrowedBySameUser)
                throw new InvalidOperationException("You cannot borrow the same book again until it is returned.");

            //set return date dynamically (e.g, 30 days from borrow date)
            bookBorrow.ReturnDate = bookBorrow.BorrowDate.AddDays(30); //Example loan period
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
          var response =  await _bookBorrowRepository.UpdateBookBorrowAsync(bookBorrow);

            if (response.Contains("failed"))
                throw new InvalidOperationException("failed to update the book borrow record.");
        }


        public async Task DeleteBookBorrowAsync(int id)
        {
            await _bookBorrowRepository.DeleteBookBorrowAsync(id);
        }

      
       
    }
}

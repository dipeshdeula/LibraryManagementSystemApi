using Application.Commands.BookCopyCommand;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.BookCopyCommandHandler
{
    public class CreateBookCopyCommandHandler : IRequestHandler<CreateBookCopyCommand, string>
    {
        private readonly IBookCopyService _bookCopyService;
        private readonly IBookService _bookService;

        public CreateBookCopyCommandHandler(IBookCopyService bookCopyService,IBookService bookService)
        {
            _bookCopyService = bookCopyService;
            _bookService = bookService;
        }

        public async Task<string> Handle(CreateBookCopyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the main book entity to get the total quantity
                var book = await _bookService.GetBooksByIdAsync(request.BookId);
                if (book == null)
                {
                    throw new System.InvalidOperationException("Book not found");
                }

                // Check if the total number of copies does not exceed the quantity specified in the main book
                var existingCopies = await _bookCopyService.GetAllBookCopiesAsync();
                var currentCopiesCount = existingCopies.Count(bc => bc.BookId == request.BookId && !bc.IsDeleted);

                if (currentCopiesCount >= book.Quantity)
                {
                    throw new System.InvalidOperationException("Cannot exceed the stock limit specified in the Book table");
                }

                // Ensure the barcode is unique
                var existingCopy = await _bookCopyService.GetBookCopyByBarcodeAsync(request.Barcode);
                if (existingCopy != null)
                {
                    throw new InvalidOperationException("Duplicate Barcode. Each copy must have a unique Barcode");
                }

                var bookCopy = new BookCopyEntity
                {
                    Barcode = request.Barcode,
                    BookId = request.BookId,
                    IsAvailable = true,
                    IsDeleted = false
                };

                var result = await _bookCopyService.AddBookCopyAsync(bookCopy);
                if (result == null)
                {
                    throw new System.InvalidOperationException("Failed to add book copy");
                }
                return "Book copy added successfully";
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in CreateBookCopyCommandHandler: {ex.Message}");
                throw;
            }
        }
    }
}

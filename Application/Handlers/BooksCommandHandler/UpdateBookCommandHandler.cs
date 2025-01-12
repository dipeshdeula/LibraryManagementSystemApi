using Application.Commands.BooksCommand;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.BooksCommandHandler
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, int>
    {
        private readonly IBookService _bookService;

        public UpdateBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<int> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetBooksByIdAsync(request.BookId);
            if (book == null)
            {
                throw new Exception("Book not found");
            }

            book.BookId = request.BookId;
            book.Title = request.Title ?? book.Title;
            book.AuthorId = request.AuthorId ?? book.AuthorId;
            book.ISBN = request.ISBN ?? book.ISBN;
            book.Genre = request.Genre ?? book.Genre;
            book.Quantity = request.Quantity ?? book.Quantity;
            // book.PublishedDate = book.PublishedDate;
            book.AvailabilityStatus = request.AvailabilityStatus ?? book.AvailabilityStatus;

            await _bookService.UpdateBooksAsync(book);
            return book.BookId;
        }
    }
}

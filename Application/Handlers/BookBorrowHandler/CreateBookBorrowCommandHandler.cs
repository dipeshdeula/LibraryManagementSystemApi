using Application.Commands.BookBorrow;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.BookBorrowHandler
{
    public class CreateBookBorrowCommandHandler : IRequestHandler<CreateBookBorrowCommand, BookBorrowEntity>
    {
        private readonly IBookBorrowService _bookBorrowService;

        public CreateBookBorrowCommandHandler(IBookBorrowService bookBorrowService)
        { 
            _bookBorrowService = bookBorrowService;
        }
        public async Task<BookBorrowEntity> Handle(CreateBookBorrowCommand request, CancellationToken cancellationToken)
        {
            if (request.BorrowDate > DateTime.Now)
                throw new ValidationException("Borrow date cannot be in the future");

            if (request.ReturnDate <= request.BorrowDate)
                throw new ValidationException("Return date must be after borrow date.");
              
            var bookBorrow = new BookBorrowEntity
            {
                UserId = request.UserId,
                BookId = request.BookId,
                BorrowDate = request.BorrowDate,
                ReturnDate = request.ReturnDate,
                Status = request.Status,
            };

            return await _bookBorrowService.CreateBookBorrowAsync(bookBorrow);

        }
    }
}

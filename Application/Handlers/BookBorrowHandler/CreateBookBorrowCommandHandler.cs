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
              
            var bookBorrow = new BookBorrowEntity
            {
                UserId = request.UserId,
            //    BookId = request.BookId ?? 0,
                Barcode = request.Barcode,
                BorrowDate = request.BorrowDate ?? DateTime.Now, //Default to current date if null
                ReturnDate = request.ReturnDate,
                DueDate = request.DueDate ?? request.BorrowDate?.AddMonths(3)?? DateTime.Now.AddMonths(3), //default to 3 months from borrow date or current date if null
              //  Status = request.Status,
            };

            return await _bookBorrowService.CreateBookBorrowAsync(bookBorrow);

        }
    }
}

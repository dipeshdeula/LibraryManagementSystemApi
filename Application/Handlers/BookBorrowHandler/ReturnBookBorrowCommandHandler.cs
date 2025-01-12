using Application.Commands.BookBorrow;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.BookBorrowHandler
{
    public class ReturnBookBorrowCommandHandler : IRequestHandler<ReturnBookBorrowCommand,BookBorrowEntity>

    {
        private readonly IBookBorrowService _bookBorrowService;

        public ReturnBookBorrowCommandHandler(IBookBorrowService bookBorrowService)
        {
            _bookBorrowService = bookBorrowService;
        }

        public async Task<BookBorrowEntity> Handle(ReturnBookBorrowCommand request, CancellationToken cancellationToken)
        {
            var result = await _bookBorrowService.ReturnBookBorrowAsync(request.UserId, request.Barcode, request.ReturnDate);
            if (result == null)
            {
                throw new InvalidOperationException("Failed to return book borrow");
            }
            return result;
        }
    }
}

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
    public class UpdateBookBorrowCommandHandler : IRequestHandler<UpdateBookBorrowCommand,int>
    {
        private readonly IBookBorrowService _bookBorrowService;

        public UpdateBookBorrowCommandHandler(IBookBorrowService bookBorrowService)
        {
            _bookBorrowService = bookBorrowService;
        }

        public async Task<int> Handle(UpdateBookBorrowCommand request, CancellationToken cancellationToken)
        {
            /*// map the command data to the entity
            var bookBorrow = new BookBorrowEntity
            {
                BorrowId = request.BorrowId,
                UserId = request.UserId,
                BookId = request.BookId,
                BorrowDate = request.BorrowDate,
                ReturnDate = request.ReturnDate,
                Status = request.Status //Status will be adjusted by the service
            };*/

            var bookBorrow = await _bookBorrowService.GetBookBorrowByIdAsync(request.BorrowId);

            if (bookBorrow == null)
            {
                throw new Exception("Book borrowed not found");
            }

            bookBorrow.BorrowId = request.BorrowId;
            bookBorrow.UserId = request.UserId;
            bookBorrow.BookId = request.BookId;
            bookBorrow.BorrowDate = request.BorrowDate;
            bookBorrow.ReturnDate = request.ReturnDate;
            bookBorrow.Status = request.Status;

            await _bookBorrowService.UpdateBookBorrowAsync(bookBorrow);
            return bookBorrow.BorrowId;
        }
    }
}

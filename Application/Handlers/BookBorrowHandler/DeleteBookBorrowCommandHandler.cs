using Application.Commands.BookBorrow;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.BookBorrowHandler
{
    public class DeleteBookBorrowCommandHandler : IRequestHandler<DeleteBookBorrowCommand, int>
    {
        private readonly IBookBorrowService _bookBorrowService;

        public DeleteBookBorrowCommandHandler(IBookBorrowService bookBorrowService)
        { 
            _bookBorrowService = bookBorrowService;
        }

        public async Task<int> Handle(DeleteBookBorrowCommand request, CancellationToken cancellationToken)
        {
            await _bookBorrowService.DeleteBookBorrowAsync(request.Id);
            return request.Id;
        }
    }
}

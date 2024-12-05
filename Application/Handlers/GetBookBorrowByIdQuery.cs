using Application.Queries;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetBookBorrowByIdQueryHandler : IRequestHandler<GetBookBorrowByIdQuery,BookBorrowEntity>

    {
        private readonly IBookBorrowService _bookBorrowService;

        public GetBookBorrowByIdQueryHandler(IBookBorrowService bookBorrowService)
        { 
            _bookBorrowService = bookBorrowService;
        }
        public async Task<BookBorrowEntity> Handle(GetBookBorrowByIdQuery request, CancellationToken cancellationToken)
        {
            return await _bookBorrowService.GetBookBorrowByIdAsync(request.id);
        }
    }
}

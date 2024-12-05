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
    public class GetAllBookBorrowQueryHandler : IRequestHandler<GetAllBookBorrowQuery,IEnumerable<BookBorrowEntity>>
    {
        private readonly IBookBorrowRepository _bookBorrowRepository;

        public GetAllBookBorrowQueryHandler(IBookBorrowRepository bookBorrowRepository)
        { 
            _bookBorrowRepository = bookBorrowRepository;
        }

        public async Task<IEnumerable<BookBorrowEntity>> Handle(GetAllBookBorrowQuery request, CancellationToken cancellationToken)
        {
            return await _bookBorrowRepository.GetAllBookBorrowAsync();
        }
    }
}

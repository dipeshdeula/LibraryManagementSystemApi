using Application.Queries.BookCopyQuery;
using Domain.Entities;
using Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.BookCopyQueryHandler
{
    public class GetAllBookCopiesQueryHandler : IRequestHandler<GetAllBookCopiesQuery, IEnumerable<BookCopyEntity>>
    {
        private readonly IBookCopyService _bookCopyService;

        public GetAllBookCopiesQueryHandler(IBookCopyService bookCopyService)
        {
            _bookCopyService = bookCopyService;
        }

        public async Task<IEnumerable<BookCopyEntity>> Handle(GetAllBookCopiesQuery request, CancellationToken cancellationToken)
        {
            return await _bookCopyService.GetAllBookCopiesAsync();
        }
    }
}

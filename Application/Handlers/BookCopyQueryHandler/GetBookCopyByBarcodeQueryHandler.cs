using Application.Queries.BookCopyQuery;
using Application.Queries.BooksQuery;
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
    public class GetBookCopyByBarcodeQueryHandler : IRequestHandler<GetBookCopyByBarcodeQuery,BookCopyEntity>
    {
        private readonly IBookCopyService _bookCopyService;

        public GetBookCopyByBarcodeQueryHandler(IBookCopyService bookCopyService)
        {
            _bookCopyService = bookCopyService;
        }

        public async Task<BookCopyEntity> Handle(GetBookCopyByBarcodeQuery request, CancellationToken cancellationToken)
        {
            return await _bookCopyService.GetBookCopyByBarcodeAsync(request.Barcode);
        }
    }
}

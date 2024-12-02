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
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery,BooksEntity>
    {
        private readonly IBookService _booService;
        public GetBookByIdQueryHandler(IBookService bookService)
        {
            _booService = bookService;
        }

        public async Task<BooksEntity> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await _booService.GetBooksByIdAsync(request.id);
        }
    }
}

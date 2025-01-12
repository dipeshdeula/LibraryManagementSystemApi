using Application.Queries.AuthorsQuery;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.AuthorsQueryHandler
{
    public class GetAuthorsByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorsEntity>
    {
        private readonly IAuthorService _authorService;

        public GetAuthorsByIdQueryHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<AuthorsEntity> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            return await _authorService.GetAuthorsByIdAsync(request.Id);
        }
    }
}

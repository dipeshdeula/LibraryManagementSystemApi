using Application.Commands.BookCopyCommand;
using Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.BookCopyCommandHandler
{
    public class DeleteBookCopyCommandHandler : IRequestHandler<DeleteBookCopyCommand, string>
    {
        private readonly IBookCopyService _bookCopyService;

        public DeleteBookCopyCommandHandler(IBookCopyService bookCopyService)
        {
            _bookCopyService = bookCopyService;
        }

        public async Task<string> Handle(DeleteBookCopyCommand request, CancellationToken cancellationToken)
        {
            await _bookCopyService.DeleteBookCopyAsync(request.Barcode, request.BookId);
            return "Book copy deleted successfully";
        }
    }
}

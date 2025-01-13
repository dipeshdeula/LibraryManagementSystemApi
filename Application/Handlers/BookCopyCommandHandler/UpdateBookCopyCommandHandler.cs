using Application.Commands.BookCopyCommand;
using Domain.Entities;
using Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.BookCopyCommandHandler
{
    public class UpdateBookCopyCommandHandler : IRequestHandler<UpdateBookCopyCommand, string>
    {
        private readonly IBookCopyService _bookCopyService;

        public UpdateBookCopyCommandHandler(IBookCopyService bookCopyService)
        {
            _bookCopyService = bookCopyService;
        }

        public async Task<string> Handle(UpdateBookCopyCommand request, CancellationToken cancellationToken)
        {
            var bookCopy = new BookCopyEntity
            {
                Barcode = request.Barcode,
                IsAvailable = request.IsAvailable,
                IsDeleted = request.IsDeleted
            };

            await _bookCopyService.UpdateBookCopyAsync(bookCopy);
            return "Book copy updated successfully!";
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.BookCopyCommand
{
    public class DeleteBookCopyCommand : IRequest<string>
    {
        public int Barcode { get; set; }
        public int BookId { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.BookBorrow
{
    public class DeleteBookBorrowCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}

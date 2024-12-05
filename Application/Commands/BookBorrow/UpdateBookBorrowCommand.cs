using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.BookBorrow
{
    public class UpdateBookBorrowCommand : IRequest<int>
    {

        public UpdateBookBorrowCommand(int borrowId, int userId, int bookId, DateTime borrowDate, DateTime returnDate, string status)
        {
            BorrowId = borrowId;
            UserId = userId;
            BookId = bookId;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
            Status = status;
        }

        public int BorrowId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; } = null!;
    }


}

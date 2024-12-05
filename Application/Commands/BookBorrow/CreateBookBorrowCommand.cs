using Domain.Entities;
using MediatR;

namespace Application.Commands.BookBorrow
{
    public class CreateBookBorrowCommand : IRequest<BookBorrowEntity>
    {
        public CreateBookBorrowCommand(int userId, int bookId, DateTime borrowDate, DateTime returnDate, string status)
        {
            UserId = userId;
            BookId = bookId;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
            Status = status;
        }

        // public int BorrowId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; } = null!;
    }

    
}

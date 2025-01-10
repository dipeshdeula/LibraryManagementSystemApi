using Domain.Entities;
using MediatR;

namespace Application.Commands.BookBorrow
{
    public class CreateBookBorrowCommand : IRequest<BookBorrowEntity>
    {
        public CreateBookBorrowCommand(int userId,int barcode, DateTime? borrowDate, DateTime? returnDate,DateTime? dueDate)
        {
            UserId = userId;
           // BookId = bookId;
            Barcode = barcode;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
            DueDate = dueDate;
           // Status = status;
        }

        // public int BorrowId { get; set; }
        public int UserId { get; set; }
        //public int? BookId { get; set; }
        public int Barcode { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? DueDate { get; set; }
       // public string Status { get; set; } = null!;
    }

    
}

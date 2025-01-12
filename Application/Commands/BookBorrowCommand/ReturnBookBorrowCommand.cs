using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.BookBorrow
{
    public class ReturnBookBorrowCommand : IRequest<BookBorrowEntity>
    {
        public ReturnBookBorrowCommand(int userId, int barcode, DateTime returnDate)
        { 
            UserId = userId;
            Barcode = barcode;
            ReturnDate = returnDate;
            
        }

        public int UserId { get; set; }
        public int Barcode { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}

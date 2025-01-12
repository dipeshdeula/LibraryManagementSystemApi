using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ReturnBookBorrowDto
    {
        public int UserId { get; set; }
        public int Barcode { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}

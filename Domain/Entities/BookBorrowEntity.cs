using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookBorrowEntity
    {
        public int BorrowId { get; set; }
        public int UserId { get; set; }
      //  public int BookId { get; set; }
        public int Barcode { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; } = null!;

     /*   public string BorrowDateFormatted => BorrowDate.ToString("yyyy-MM-dd");
        public string ReturnDateFormatted => ReturnDate.ToString("yyyy-MM-dd");
*/


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ReviewsEntity
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; } = string.Empty;
        public DateOnly ReviewDate { get; set; }

        //Helper property to get DateOnly
        //public DateOnly ReviewDateOnly => DateOnly.FromDateTime(ReviewDate);


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ReviewDtos
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; } = string.Empty;

        // Adjusted property to handle JSON compatibility
        public string ReviewDate { get; set; } = string.Empty;

        // Utility to convert to/from DateOnly
        public DateOnly GetReviewDateAsDateOnly() => DateOnly.ParseExact(ReviewDate, "yyyy-MM-dd");
        public void SetReviewDateFromDateOnly(DateOnly date) => ReviewDate = date.ToString("yyyy-MM-dd");

    }
}

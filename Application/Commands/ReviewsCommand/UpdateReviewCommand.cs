using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ReviewsCommand
{
    public class UpdateReviewCommand : IRequest<int>
    {
        public UpdateReviewCommand(int reviewId, int userId, int bookId, int rating, DateOnly reviewDate, string comments)
        {
            ReviewId = reviewId;
            UserId = userId;
            BookId = bookId;
            Rating = rating;
            ReviewDate = reviewDate;
            Comments = comments;

        }

        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public DateOnly ReviewDate { get; set; }
    }
}

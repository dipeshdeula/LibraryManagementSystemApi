using Domain.Entities;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Commands
{
    public class CreateReviewCommand : IRequest<ReviewsEntity>
    {
        public CreateReviewCommand(int userId,int bookId,int rating,DateOnly reviewDate,string comments)
        {
           
            UserId = userId;
            BookId = bookId;
            Rating = rating;
            ReviewDate = reviewDate;
            Comments = comments;
           
        
        }

        //public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public DateOnly ReviewDate { get; set; }
    }
}

using Application.Commands.ReviewsCommand;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ReviewsCommandHandler
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, int>
    {
        private readonly IReviewService _reviewService;
        public UpdateReviewCommandHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<int> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _reviewService.GetReviewByUserIdAsync(request.ReviewId);
            if (review == null)
            {
                throw new Exception("Review not found");
            }

            review.ReviewId = request.ReviewId;
            review.UserId = request.UserId;
            review.BookId = request.BookId;
            review.Rating = request.Rating;
            review.ReviewDate = request.ReviewDate;
            review.Comments = request.Comments;

            await _reviewService.UpdateReviewAsync(review);
            return review.ReviewId;
        }
    }
}

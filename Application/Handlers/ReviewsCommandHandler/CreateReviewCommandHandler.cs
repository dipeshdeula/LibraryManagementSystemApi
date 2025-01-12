using Application.Commands.ReviewsCommand;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ReviewsCommandHandler
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewsEntity>

    {
        private readonly IReviewService _reviewService;
        public CreateReviewCommandHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;

        }
        public async Task<ReviewsEntity> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = new ReviewsEntity
            {
                UserId = request.UserId,
                BookId = request.BookId,
                Rating = request.Rating,
                ReviewDate = request.ReviewDate,
                Comments = request.Comments

            };
            return await _reviewService.CreateReviewAsync(review);



        }
    }
}

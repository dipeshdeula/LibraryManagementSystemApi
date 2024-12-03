using Application.Queries;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetReviewByUserIdQueryHandler : IRequestHandler<GetReviewByUserIdQuery, ReviewsEntity>
    {
        private readonly IReviewService _reviewService;
        public GetReviewByUserIdQueryHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        public async Task<ReviewsEntity> Handle(GetReviewByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _reviewService.GetReviewByUserIdAsync(request.id);
        }
    }
}

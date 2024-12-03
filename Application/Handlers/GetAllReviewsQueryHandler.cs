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
    public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, IEnumerable<ReviewsEntity>>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetAllReviewsQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<ReviewsEntity>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
        {
            return await _reviewRepository.GetAllReviewAsync();
        }
    }
}

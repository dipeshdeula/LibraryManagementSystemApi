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
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, int>
    {
        private readonly IReviewService _reviewService;

        public DeleteReviewCommandHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<int> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            await _reviewService.DeleteReviewAsync(request.Id);
            return request.Id;
        }
    }
}

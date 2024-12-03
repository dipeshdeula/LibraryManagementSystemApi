using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<ReviewsEntity>> GetAllReviewAsync()
        {
            return await _reviewRepository.GetAllReviewAsync();
        }

        public async Task<ReviewsEntity> GetReviewByUserIdAsync(int id)
        {
            return await _reviewRepository.GetReviewByUserId(id);
        }

        public async Task<ReviewsEntity> CreateReviewAsync(ReviewsEntity review)
        {
            await _reviewRepository.AddReviewAsync(review);
            return review;
        }

        public async Task UpdateReviewAsync(ReviewsEntity review)
        {
            await _reviewRepository.UpdateReviewAsync(review);
        }
        

        public async Task DeleteReviewAsync(int id)
        {
            await _reviewRepository.DeleteReviewAsync(id);

        }

       
    }
}

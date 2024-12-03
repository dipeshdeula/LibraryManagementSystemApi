using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewsEntity>> GetAllReviewAsync();
        Task<ReviewsEntity> GetReviewByUserIdAsync(int id);
        Task<ReviewsEntity> CreateReviewAsync(ReviewsEntity review);
        Task UpdateReviewAsync(ReviewsEntity review);
        Task DeleteReviewAsync(int id);
    }
}

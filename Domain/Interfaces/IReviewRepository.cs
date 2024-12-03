using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<ReviewsEntity>> GetAllReviewAsync();
        Task<ReviewsEntity> GetReviewByUserId(int id);
        Task<string> AddReviewAsync(ReviewsEntity review);
        Task<string> UpdateReviewAsync(ReviewsEntity review);
        Task<string> DeleteReviewAsync(int id);
    }
}

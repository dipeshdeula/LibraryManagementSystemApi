using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IConfiguration configuration;
        private readonly string _connectionString;

        public ReviewRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ReviewsEntity>> GetAllReviewAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "SE");

                return await connection.QueryAsync<ReviewsEntity>(
                    "[SP_Review]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);

            }
        }

        public async Task<ReviewsEntity> GetReviewByUserId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "SE");
                parameters.Add("@ReviewId", id);

                return await connection.QueryFirstOrDefaultAsync<ReviewsEntity>(
                    "[SP_Review]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            }
        }

        public async Task<string> AddReviewAsync(ReviewsEntity review)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "I");
                parameters.Add("@ReviewId", review.ReviewId);
                parameters.Add("@UserId", review.UserId);
                parameters.Add("@BookId", review.BookId);
                parameters.Add("@Rating", review.Rating);
                parameters.Add("@ReviewDate", review.ReviewDate.ToDateTime(TimeOnly.MinValue));
                parameters.Add("Comments", review.Comments);
              

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[SP_Review]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);


                return result?.Msg ?? "Operation failed";

            }
        }

        public async Task<string> UpdateReviewAsync(ReviewsEntity review)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "U");
                parameters.Add("@ReviewId", review.ReviewId);
                parameters.Add("@UserId", review.UserId);
                parameters.Add("@BookId", review.BookId);
                parameters.Add("@Rating", review.Rating);
                parameters.Add("Comments", review.Comments);
                parameters.Add("@ReviewDate", review.ReviewDate.ToDateTime(TimeOnly.MinValue));


                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[SP_Review]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
                return result?.Msg ?? "Operation failed";

            }
        }

        public async Task<string> DeleteReviewAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "D");
                parameters.Add("@ReviewId", id);

                var result = await connection.QueryFirstOrDefaultAsync(
                    "[SP_Review]",
                    parameters,
                    commandType:System.Data.CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed";
            }
        }

    }
}

using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
   public class BookRepository : IBookRepository
    {
        private readonly IConfiguration configuration;
        private readonly string _connectionString;

        public BookRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<BooksEntity>> GetAllBooksAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "SE");

                return await connection.QueryAsync<BooksEntity>(
                    "[SP_Books]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<BooksEntity> GetBooksByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "SE");
                parameters.Add("@BookId", id);

                return await connection.QueryFirstOrDefaultAsync<BooksEntity>(
                    "[SP_Books]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<string> AddBooksAsync(BooksEntity books)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "I");
                parameters.Add("@BookId", books.BookId);
                parameters.Add("@Title", books.Title);
                parameters.Add("@AuthorId", books.AuthorId);
                parameters.Add("@ISBN", books.ISBN);
                parameters.Add("@Quantity", books.Quantity);
                parameters.Add("@Genre", books.Genre);
                parameters.Add("@PublishedDate", books.PublishedDate.ToDateTime(TimeOnly.MinValue));
                parameters.Add("@AvailabilityStatus", books.AvailabilityStatus);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[SP_Books]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed";
            }
        }

        public async Task<string> UpdateBooksAsync(BooksEntity books)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "U");
                parameters.Add("@BookId", books.BookId);
                parameters.Add("@Title", books.Title);
                parameters.Add("@AuthorId", books.AuthorId);
                parameters.Add("@ISBN", books.ISBN);
                parameters.Add("@Quantity", books.Quantity);
                parameters.Add("@Genre", books.Genre);
                parameters.Add("@PublishedDate", books.PublishedDate.ToDateTime(TimeOnly.MinValue));
                parameters.Add("@AvailabilityStatus", books.AvailabilityStatus);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[SP_Books]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed";
            }
        }

        public async Task<string> DeleteBooksAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "D");
                parameters.Add("@BookId", id);

               var result = await connection.QueryFirstOrDefaultAsync(
                    "[SP_Books]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed";
            }
        }
    }
}

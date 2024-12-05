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
   public class BookBorrowRepository : IBookBorrowRepository
    {
        private readonly IConfiguration configuration;
        private readonly string _connectionString;

        public BookBorrowRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<BookBorrowEntity>> GetAllBookBorrowAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "SE");

                return await connection.QueryAsync<BookBorrowEntity>(
                    "[SP_Borrow_Books]",
                    parameters,
                    commandType:System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<BookBorrowEntity> GetBookBorrowAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "SE");
                parameters.Add("BorrowId", id);

                return await connection.QueryFirstOrDefaultAsync<BookBorrowEntity>(
                    "[SP_Borrow_Books]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<string> AddBookBorrowAsync(BookBorrowEntity bookBorrow)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "I");
                parameters.Add("@BorrowId", bookBorrow.BorrowId);
                parameters.Add("@UserId",bookBorrow.UserId);
                parameters.Add("@BookId",bookBorrow.BookId);
                parameters.Add("@BorrowDate", bookBorrow.BorrowDate);
                parameters.Add("@ReturnDate", bookBorrow.ReturnDate);
                parameters.Add("@Status", bookBorrow.Status);

                try
                {

                    var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                        "[SP_Borrow_Books]",
                        parameters,
                        commandType: System.Data.CommandType.StoredProcedure);

                    return result?.Msg ?? "Operation failed";
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("This book is already borrowed"))
                        return "Book is currently borrowed by the user.";
                    throw;
                }
            }
        }

        public async Task<string> UpdateBookBorrowAsync(BookBorrowEntity bookBorrow)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "U");
                parameters.Add("@BorrowId", bookBorrow.BorrowId);
                parameters.Add("@UserId", bookBorrow.UserId);
                parameters.Add("@BookId", bookBorrow.BookId);
                parameters.Add("@BorrowDate", bookBorrow.BorrowDate);
                parameters.Add("@ReturnDate", bookBorrow.ReturnDate);
                parameters.Add("@Status", bookBorrow.Status);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[SP_Borrow_Books]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed";
            }
        }

        public async Task<string> DeleteBookBorrowAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "D");
                parameters.Add("@BorrowId", id);

                var result = await connection.QueryFirstOrDefaultAsync(
                    "[SP_Borrow_Books]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed";
            }
        }

        
    }
}

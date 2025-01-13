using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BookCopyRepository : IBookCopyRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public BookCopyRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            
        }

        public async Task<IEnumerable<BookCopyEntity>> GetAllBookCopiesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "SE");

                return await connection.QueryAsync<BookCopyEntity>(
                    "[dbo].[SP_BookCopies]",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<BookCopyEntity> GetBookCopyByBarcodeAsync(int barcode)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "SE");
                parameters.Add("@Barcode", barcode);

                return await connection.QueryFirstOrDefaultAsync<BookCopyEntity>(
                    "[dbo].[SP_BookCopies]",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<string> AddBookCopyAsync(BookCopyEntity bookCopy)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@flag", "I");
                    parameters.Add("@Barcode", bookCopy.Barcode);
                    parameters.Add("@BookId", bookCopy.BookId);
                    parameters.Add("@Quantity", 1);

                    var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                        "[dbo].[SP_BookCopies]",
                        parameters,
                        commandType: CommandType.StoredProcedure);

                    return result?.Msg ?? "Operation failed";
                }
            }
            catch (Exception ex)
            {
                //Log the exception 
                Console.WriteLine($"Error in AddBookCopyAsync:{ex.Message}");
                throw;
            }
        }

        public async Task<string> UpdateBookCopyAsync(BookCopyEntity bookCopy)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "U");
                parameters.Add("@Barcode", bookCopy.Barcode);
                parameters.Add("@IsAvailable", bookCopy.IsAvailable);
                parameters.Add("@IsDeleted", bookCopy.IsDeleted);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[dbo].[SP_BookCopies]",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed";
            }
        }

        public async Task<string> DeleteBookCopyAsync(int barcode, int bookId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "D");
                parameters.Add("@Barcode", barcode);
                parameters.Add("@BookId", bookId);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[dbo].[SP_BookCopies]",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed";
            }
        }
    }
}

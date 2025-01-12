using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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

        public async Task<IEnumerable<BookBorrowEntity?>> GetAllBookBorrowAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "SE");

                return await connection.QueryAsync<BookBorrowEntity>(
                    "[SP_ManageBookBorrow]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
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
                    "[SP_ManageBookBorrow]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<string> AddBookBorrowAsync(BookBorrowEntity bookBorrow)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "BO");
                //parameters.Add("@BorrowId", bookBorrow.BorrowId);
                parameters.Add("@UserId", bookBorrow.UserId);
                // parameters.Add("@BookId",bookBorrow.BookId);
                parameters.Add("@Barcode", bookBorrow.Barcode);
                parameters.Add("@BorrowDate", bookBorrow.BorrowDate);
                // parameters.Add("@ReturnDate", bookBorrow.ReturnDate);
                // parameters.Add("@DueDate", bookBorrow.DueDate);
                //parameters.Add("@Status", bookBorrow.Status);

                try
                {

                    var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                        "[SP_ManageBookBorrow]",
                        parameters,
                        commandType: System.Data.CommandType.StoredProcedure);

                    return result?.Msg ?? "Operation failed";
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("This book copy is not available for borrowing"))
                        return "This book copy is not available for borrowing.";
                    if (ex.Message.Contains("You cannot borrow another copy of the same book without returning the previous one"))
                        return "You cannot borrow another copy of the same book without returning the previous one.";
                    if (ex.Message.Contains("You cannot borrow new books while you have overdue books"))
                        return "You cannot borrow new books while you have overdue books.";
                    throw;
                }
            }
        }

        public async Task<string> ReturnBookBorrowAsync(int userId, int barcode, DateTime? returnDate)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "RE");
                parameters.Add("UserId", userId);
                parameters.Add("@Barcode", barcode);
                parameters.Add("@ReturnDate", returnDate);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[SP_ManageBookBorrow]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed";
            }

        }

        public async Task<string> MarkOverdueBooksAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "OV");

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                   "[SP_ManageBookBorrow]",
                   parameters,
                   commandType: System.Data.CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed";
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
                //  parameters.Add("@BookId", bookBorrow.BookId);
                parameters.Add("@BorrowDate", bookBorrow.BorrowDate);
                parameters.Add("@ReturnDate", bookBorrow.ReturnDate);
                //  parameters.Add("@Status", bookBorrow.Status);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[SP_ManageBookBorrow]",
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
                    "[SP_ManageBookBorrow]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed";
            }
        }

        public async Task<bool> IsBookCopyAvailable(int barcode)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "SE");
                parameters.Add("@Barcode", barcode);

                var result = await connection.QueryFirstOrDefaultAsync<BookBorrowEntity>(
                    "[SP_ManageBookBorrow]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
                return result != null;
            }
        }


    }
}

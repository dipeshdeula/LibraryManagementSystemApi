using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration configuration;
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<UserEntity> AuthenticateAsync(string username, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password";
                var parameters = new { UserName = username, Password = password };

                return await connection.QueryFirstOrDefaultAsync<UserEntity>(query, parameters);
            }

        }

        public async Task<IEnumerable<UserEntity>> GetAllUserAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "SE");

                return await connection.QueryAsync<UserEntity>(

                    "[SP_Users]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<UserEntity> GetUserByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "SE");
                parameters.Add("@UserID", id);

                return await connection.QueryFirstOrDefaultAsync<UserEntity>(
                    "[SP_Users]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<string> AddUserAsync(UserEntity user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "I");
                parameters.Add("@UserId", user.UserID);
                parameters.Add("@UserName", user.UserName);
                parameters.Add("@Password", user.Password);
                parameters.Add("@Email", user.Email);
                parameters.Add("@UserProfile", user.UserProfile);
                parameters.Add("@FullName", user.FullName);
                parameters.Add("@Role", user.Role);
                parameters.Add("@Phone", user.Phone);
                parameters.Add("@LoginStatus", user.LoginStatus);


                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[SP_Users]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed";

            }
        }

        public async Task<string> UpdateUserAsync(UserEntity user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "U");
                parameters.Add("@UserID", user.UserID);
                parameters.Add("@UserName", user.UserName);
                parameters.Add("@Password", user.Password);
                parameters.Add("@Email", user.Email);
                parameters.Add("@UserProfile", user.UserProfile);
                parameters.Add("@FullName", user.FullName);
                parameters.Add("@Role", user.Role);
                parameters.Add("@Phone", user.Phone);
                parameters.Add("@LoginStatus", user.LoginStatus);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[SP_Users]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed";
            }
        }

        public async Task<string> DeleteUserAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "D");
                parameters.Add("@UserID", id);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[SP_Users]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed";
            }
        }
    }
}

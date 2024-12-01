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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IConfiguration configuration;
        private readonly string _connectionString;

        public AuthorRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<AuthorsEntity>> GetAllAuthorsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "SE");

                return await connection.QueryAsync<AuthorsEntity>(
                    "[SP_Authors]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<AuthorsEntity> GetAuthorsByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            { 
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "SE");
                parameters.Add("@AuthorID", id);

                return await connection.QueryFirstOrDefaultAsync<AuthorsEntity>(
                    "[SP_Authors]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<string> AddAuthorAsync(AuthorsEntity author)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "I");
                parameters.Add("@AuthorId", author.AuthorId);
                parameters.Add("@AuthorName", author.AuthorName);
                parameters.Add("@Biography", author.Biography);
                parameters.Add("@AuthorProfile", author.AuthorProfile);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[SP_Authors]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);

                return result?.Msg??"Operation failed";
            }
        }

        public async Task<string> UpdateAuthorAsync(AuthorsEntity author)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "U");
                parameters.Add("@AuthorId", author.AuthorId);
                parameters.Add("@AuthorName", author.AuthorName);
                parameters.Add("@Biography", author.Biography);
                parameters.Add("@AuthorProfile", author.AuthorProfile);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[SP_Authors]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed";
            }
        }

        public async Task<string> DeleteAuthorAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "D");
                parameters.Add("@AuthorId", id);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[SP_Authors]",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed";
            }
        }
    }
}

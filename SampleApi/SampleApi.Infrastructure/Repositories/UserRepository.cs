using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SampleApi.Application.Interfaces.Repositories;
using SampleApi.Domain.Entities.Identity;
using SampleApi.Infrastructure.Configurations;
using System.Data;

namespace SampleApi.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IOptions<DatabaseOptions> options)
    {
        _connectionString = options.Value.DefaultConnection;

        if (string.IsNullOrWhiteSpace(_connectionString))
            throw new InvalidOperationException(
                "Database connection string is missing."
            );
    }

    public async Task<int> CreateAsync(User user)
    {
        await using var connection = new SqlConnection(_connectionString);

        return await connection.ExecuteScalarAsync<int>(
            "identity.User_Insert",
            new
            {
                user.Username,
                user.CreatedById
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await using var connection = new SqlConnection(_connectionString);

        var affectedRows = await connection.ExecuteAsync(
            "identity.User_Delete",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );

        return affectedRows > 0;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        await using var connection = new SqlConnection(_connectionString);

        return await connection.QuerySingleOrDefaultAsync<User>(
            "identity.User_SelectById",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<bool> UpdateAsync(User user)
    {
        await using var connection = new SqlConnection(_connectionString);

        var affectedRows = await connection.ExecuteAsync(
            "identity.User_Update",
            new
            {
                user.Id,
                user.Username
            },
            commandType: CommandType.StoredProcedure
        );

        return affectedRows > 0;
    }
}
using Dapper;
using InvoiceManagementAPI.Data;
using InvoiceManagementAPI.Models;

namespace InvoiceManagementAPI.Repositories
{
    public class UserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var sql = "SELECT * FROM Users WHERE Email = @Email";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Email = email });
        }

        public async Task CreateAsync(User user)
        {
            var sql = @"INSERT INTO Users (Name, Email, PasswordHash, Role)
                        VALUES (@Name, @Email, @PasswordHash, @Role)";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, user);
        }
    }
}

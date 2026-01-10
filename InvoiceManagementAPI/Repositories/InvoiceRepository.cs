namespace InvoiceManagementAPI.Repositories
{
    using Dapper;
    using InvoiceManagementAPI.Data;
    using InvoiceManagementAPI.DTOs.Invoice;
    using InvoiceManagementAPI.Models;

    public class InvoiceRepository
    {
        private readonly DapperContext _context;

        public InvoiceRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            var sql = "SELECT * FROM Invoices";
            using var conn = _context.CreateConnection();
            return await conn.QueryAsync<Invoice>(sql);
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Invoices WHERE InvoiceId=@id";
            using var conn = _context.CreateConnection();
            return await conn.QuerySingleOrDefaultAsync<Invoice>(sql, new { id });
        }

        public async Task CreateAsync(Invoice invoice)
        {
            var sql = @"INSERT INTO Invoices 
                    (InvoiceNumber, CustomerName, Amount, Status)
                    VALUES (@InvoiceNumber, @CustomerName, @Amount, @Status)";
            using var conn = _context.CreateConnection();
            await conn.ExecuteAsync(sql, invoice);
        }

        public async Task UpdateAsync(int id, UpdateInvoiceDto dto)
        {
            var sql = @"UPDATE Invoices 
                    SET CustomerName=@CustomerName,
                        Amount=@Amount,
                        Status=@Status
                    WHERE InvoiceId=@id";
            using var conn = _context.CreateConnection();
            await conn.ExecuteAsync(sql, new { dto.CustomerName, dto.Amount, dto.Status, id });
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM Invoices WHERE InvoiceId=@id";
            using var conn = _context.CreateConnection();
            await conn.ExecuteAsync(sql, new { id });
        }
    }

}

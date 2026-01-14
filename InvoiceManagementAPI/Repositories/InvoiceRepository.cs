using Dapper;
using InvoiceManagementAPI.Data;
using InvoiceManagementAPI.Models;

namespace InvoiceManagementAPI.Repositories
{
    public class InvoiceRepository
    {
        private readonly DapperContext _context;

        public InvoiceRepository(DapperContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task CreateAsync(Invoice invoice)
        {
            var sql = @"
                INSERT INTO Invoices
                (InvoiceNumber, CustomerName, Amount, Status, CreatedDate)
                VALUES
                (@InvoiceNumber, @CustomerName, @Amount, @Status, @CreatedDate)
            ";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, invoice);
        }

        // UPDATE STATUS (Draft → Paid / Cancelled)
        public async Task UpdateStatusAsync(int invoiceId, string status)
        {
            var sql = @"
                UPDATE Invoices
                SET Status = @Status
                WHERE InvoiceId = @InvoiceId
            ";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, new
            {
                Status = status,
                InvoiceId = invoiceId
            });
        }

        // DELETE
        public async Task DeleteAsync(int invoiceId)
        {
            var sql = "DELETE FROM Invoices WHERE InvoiceId = @InvoiceId";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, new { InvoiceId = invoiceId });
        }
    }
}


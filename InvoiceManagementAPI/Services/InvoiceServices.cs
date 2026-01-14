using InvoiceManagementAPI.BackgroundTasks;
using InvoiceManagementAPI.Repositories;
using InvoiceManagementAPI.DTOs.Invoice;
using InvoiceManagementAPI.Models;

namespace InvoiceManagementAPI.Services
{
    public class InvoiceService
    {
        private readonly InvoiceRepository _invoiceRepository;
        private readonly InvoicePaymentLogger _paymentLogger;

        public InvoiceService(
            InvoiceRepository invoiceRepository,
            InvoicePaymentLogger paymentLogger)
        {
            _invoiceRepository = invoiceRepository;
            _paymentLogger = paymentLogger;
        }

        // CREATE
        public async Task CreateInvoiceAsync(CreateInvoiceDto dto)
        {
            var invoice = new Invoice
            {
                InvoiceNumber = dto.InvoiceNumber,
                CustomerName = dto.CustomerName,
                Amount = dto.Amount,
                Status = "Draft",
                CreatedDate = DateTime.UtcNow
            };

            await _invoiceRepository.CreateAsync(invoice);
        }

        // MARK AS PAID
        public async Task MarkInvoiceAsPaidAsync(int invoiceId)
        {
            // ✅ Update status in DB
            await _invoiceRepository.UpdateStatusAsync(invoiceId, "Paid");

            // ✅ Log payment asynchronously
            await _paymentLogger.LogAsync(invoiceId);
        }

        // DELETE
        public async Task DeleteInvoiceAsync(int invoiceId)
        {
            await _invoiceRepository.DeleteAsync(invoiceId);
        }
    }
}



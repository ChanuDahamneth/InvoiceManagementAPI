using InvoiceManagementAPI.BackgroundTasks;
using InvoiceManagementAPI.Repositories;

namespace InvoiceManagementAPI.Services;

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

    public void CreateInvoice()
    {
        // Repository handles DB insert
    }

    public async Task MarkInvoiceAsPaidAsync(int invoiceId)
    {
        // Repository would update invoice status here

        // Async background logging
        await _paymentLogger.LogAsync(invoiceId);
    }

    public void DeleteInvoice(int invoiceId)
    {
        // Repository handles delete
    }
}



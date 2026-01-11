using InvoiceManagementAPI.Services;
using InvoiceManagementAPI.BackgroundTasks;
using Microsoft.AspNetCore.Mvc;
using InvoiceManagementAPI.DTOs.Invoice;

namespace InvoiceManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly InvoiceService _invoiceService;
    private readonly InvoicePaymentLogger _paymentLogger;

    public InvoicesController(
        InvoiceService invoiceService,
        InvoicePaymentLogger paymentLogger)
    {
        _invoiceService = invoiceService;
        _paymentLogger = paymentLogger; // ✅ Injected instance
    }

    [HttpPost]
    public IActionResult CreateInvoice(CreateInvoiceDto dto)
    {
        _invoiceService.CreateInvoice(); // Minimal logic for now
        return Ok(new { message = "Invoice created" });
    }

    [HttpPut("{invoiceId}/pay")]
    public async Task<IActionResult> MarkAsPaid(int invoiceId)
    {
        // ✅ Call on instance
        await _paymentLogger.LogAsync(invoiceId);

        // Or you could use the service instead:
        // await _invoiceService.MarkInvoiceAsPaidAsync(invoiceId);

        return Ok(new { message = $"Invoice {invoiceId} marked as paid." });
    }

    [HttpDelete("{invoiceId}")]
    public IActionResult DeleteInvoice(int invoiceId)
    {
        _invoiceService.DeleteInvoice(invoiceId);
        return Ok(new { message = $"Invoice {invoiceId} deleted." });
    }
}

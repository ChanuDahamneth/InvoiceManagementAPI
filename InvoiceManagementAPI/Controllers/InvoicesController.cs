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
    public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceDto dto)
    {
        await _invoiceService.CreateInvoiceAsync(dto);
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoice(int id)
    {
        await _invoiceService.DeleteInvoiceAsync(id);
        return Ok(new { message = "Invoice deleted" });
    }

}

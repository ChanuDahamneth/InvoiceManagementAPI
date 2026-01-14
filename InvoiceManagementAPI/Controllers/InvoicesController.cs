using InvoiceManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using InvoiceManagementAPI.DTOs.Invoice;

namespace InvoiceManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly InvoiceService _invoiceService;

    public InvoicesController(InvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    // POST: /api/invoices
    [HttpPost]
    public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceDto dto)
    {
        await _invoiceService.CreateInvoiceAsync(dto);
        return Ok(new { message = "Invoice created" });
    }

    // PUT: /api/invoices/{invoiceId}/pay
    [HttpPut("{invoiceId}/pay")]
    public async Task<IActionResult> MarkAsPaid(int invoiceId)
    {
        await _invoiceService.MarkInvoiceAsPaidAsync(invoiceId);
        return Ok(new { message = $"Invoice {invoiceId} marked as paid." });
    }

    // DELETE: /api/invoices/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoice(int id)
    {
        await _invoiceService.DeleteInvoiceAsync(id);
        return Ok(new { message = "Invoice deleted" });
    }
}

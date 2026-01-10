namespace InvoiceManagementAPI.Controllers
{
    using InvoiceManagementAPI.BackgroundTasks;
    using InvoiceManagementAPI.DTOs.Invoice;
    using InvoiceManagementAPI.Models;
    using InvoiceManagementAPI.Repositories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    [Route("api/invoices")]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoiceRepository _repo;

        public InvoicesController(InvoiceRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var invoice = await _repo.GetByIdAsync(id);
            if (invoice == null) return NotFound();
            return Ok(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInvoiceDto dto)
        {
            var invoice = new Invoice
            {
                InvoiceNumber = dto.InvoiceNumber,
                CustomerName = dto.CustomerName,
                Amount = dto.Amount,
                Status = "Draft"
            };

            await _repo.CreateAsync(invoice);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateInvoiceDto dto)
        {
            await _repo.UpdateAsync(id, dto);

            if (dto.Status == "Paid")
                await InvoicePaymentLogger.LogAsync(id);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return Ok();
        }
    }

}

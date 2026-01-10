namespace InvoiceManagementAPI.DTOs.Invoice
{
    public class CreateInvoiceDto
    {
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal Amount { get; set; }
    }
}
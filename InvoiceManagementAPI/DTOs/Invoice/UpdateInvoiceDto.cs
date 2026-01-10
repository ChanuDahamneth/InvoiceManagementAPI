namespace InvoiceManagementAPI.DTOs.Invoice
{
    public class UpdateInvoiceDto
    {
        public string CustomerName { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }

}

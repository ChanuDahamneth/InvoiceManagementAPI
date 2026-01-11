namespace InvoiceManagementAPI.BackgroundTasks;

public class InvoicePaymentLogger
{
    // Make sure the method is PUBLIC and ASYNC
    public async Task LogAsync(int invoiceId)
    {
        // Simulate async background logging
        await Task.Run(() =>
        {
            Console.WriteLine($"Invoice {invoiceId} marked as PAID at {DateTime.UtcNow}");
        });
    }
}

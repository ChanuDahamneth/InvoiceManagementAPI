namespace InvoiceManagementAPI.BackgroundTasks
{
    public static class InvoicePaymentLogger
    {
        public static Task LogAsync(int invoiceId)
        {
            return Task.Run(() =>
            {
                File.AppendAllText(
                    "payment_log.txt",
                    $"Invoice {invoiceId} paid at {DateTime.Now}\n"
                );
            });
        }
    }

}

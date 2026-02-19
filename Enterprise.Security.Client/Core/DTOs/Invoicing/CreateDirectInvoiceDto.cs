namespace Enterprise.Security.Client.Core.DTOs.Invoicing
{
    public class CreateDirectInvoiceItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateDirectInvoiceDto
    {
        public Guid ClientId { get; set; }
        public List<CreateDirectInvoiceItemDto> Items { get; set; } = new();
    }
}

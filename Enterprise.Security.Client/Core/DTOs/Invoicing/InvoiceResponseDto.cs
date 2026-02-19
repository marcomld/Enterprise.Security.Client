namespace Enterprise.Security.Client.Core.DTOs.Invoicing
{
    public class InvoiceItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductSku { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TaxRate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }
    }

    public class InvoiceResponseDto
    {
        public Guid Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string? SellerName { get; set; } // Nullable, null si fue automática (aunque ya lo corregimos en el backend)
        public Guid? OrderId { get; set; }      // Nullable, null si es venta directa
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public List<InvoiceItemDto> Items { get; set; } = new();
    }
}

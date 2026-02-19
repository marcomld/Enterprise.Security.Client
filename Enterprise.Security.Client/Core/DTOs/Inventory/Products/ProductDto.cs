namespace Enterprise.Security.Client.Core.DTOs.Inventory.Products
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public decimal TaxRate { get; set; }
        public int StockQuantity { get; set; }
        public int MinStockLevel { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateProductDto
    {
        public Guid CategoryId { get; set; }
        public string SKU { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public decimal TaxRate { get; set; }
        public int StockQuantity { get; set; }
        public int MinStockLevel { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class UpdateProductDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public decimal TaxRate { get; set; }
        public int MinStockLevel { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }

    public class AdjustStockDto
    {
        public Guid ProductId { get; set; }
        public int QuantityAdjustment { get; set; } // Puede ser positivo (entrada) o negativo (salida)
        public string Reason { get; set; } = string.Empty; // Motivo del ajuste (Auditoria)
    }
}

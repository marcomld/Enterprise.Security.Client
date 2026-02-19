namespace Enterprise.Security.Client.Core.DTOs.Orders
{
    public class CreateOrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateOrderDto
    {
        public List<CreateOrderItemDto> Items { get; set; } = new();
        public string? Notes { get; set; }
    }

    // Este es interno para el Frontend (Visualización en el carrito)
    public class CartItem
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductSku { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }
        public int MaxStock { get; set; } // Para no dejarle comprar más del stock real

        public decimal SubTotal => UnitPrice * Quantity;
    }
}

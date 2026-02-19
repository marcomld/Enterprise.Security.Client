namespace Enterprise.Security.Client.Core.DTOs.Orders
{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }

        // Eliminamos OrderNumber como propiedad settable y la hacemos calculada
        // Si el backend no manda número, usamos los primeros 8 caracteres del ID.
        public string OrderNumber => Id.ToString().Substring(0, 8).ToUpper();

        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }

        // Eliminamos TotalItems como propiedad settable y la hacemos calculada
        // Sumamos la cantidad de productos en la lista Items
        public int TotalItems => Items?.Sum(x => x.Quantity) ?? 0;

        public List<OrderItemDto> Items { get; set; } = new();
        public string? Notes { get; set; }
    }

    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}

using Enterprise.Security.Client.Core.DTOs.Inventory.Products;
using Enterprise.Security.Client.Core.DTOs.Orders;

namespace Enterprise.Security.Client.Core.Interfaces
{
    public interface ICartService
    {
        event Action OnChange; // Evento para notificar cambios en el carrito a la UI
        List<CartItem> CurrentItems { get; } // Solo lectura para la UI
        Task RemoveFromCart(Guid productId);
        Task ClearCart();
        Task InitializeCart(); // Cargar desde localstorage al iniciar
        Task AddToCart(ProductDto product, int quantity = 1);
        Task UpdateQuantity(Guid productId, int quantity);

    }
}

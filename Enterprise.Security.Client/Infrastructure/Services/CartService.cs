using Enterprise.Security.Client.Core.DTOs.Inventory.Products;
using Enterprise.Security.Client.Core.DTOs.Orders;
using Enterprise.Security.Client.Core.Interfaces;
using Microsoft.AspNetCore.Components.Authorization; // Necesario para saber quién es el usuario
using System.Security.Claims;

namespace Enterprise.Security.Client.Infrastructure.Services;

public class CartService : ICartService
{
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authStateProvider;
    private List<CartItem> _cart = new();
    private string _storageKey = "cart_guest"; // Clave por defecto

    public event Action? OnChange;
    public List<CartItem> CurrentItems => _cart;

    public CartService(ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
    {
        _localStorage = localStorage;
        _authStateProvider = authStateProvider;
    }

    public async Task InitializeCart()
    {
        // 1. Obtener el usuario actual para generar una clave única
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity != null && user.Identity.IsAuthenticated)
        {
            // Buscamos el ID del usuario (Claim "sub" o "nameid")
            var userId = user.FindFirst(c => c.Type == "sub" || c.Type == ClaimTypes.NameIdentifier)?.Value;
            _storageKey = $"cart_user_{userId}";
        }
        else
        {
            _storageKey = "cart_guest";
        }

        // 2. Cargar el carrito específico de ese usuario
        var savedCart = await _localStorage.GetItemAsync<List<CartItem>>(_storageKey);
        if (savedCart != null)
        {
            _cart = savedCart;
            NotifyStateChanged();
        }
        else
        {
            _cart = new List<CartItem>(); // Limpiar si no hay nada guardado para este usuario
        }
    }

    // Modificamos para aceptar cantidad inicial
    public async Task AddToCart(ProductDto product, int quantity = 1)
    {
        var existingItem = _cart.FirstOrDefault(x => x.ProductId == product.Id);

        if (existingItem != null)
        {
            // Validar stock máximo
            if (existingItem.Quantity + quantity <= product.StockQuantity)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                // Opcional: Ajustar al máximo disponible
                existingItem.Quantity = product.StockQuantity;
            }
        }
        else
        {
            _cart.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductSku = product.SKU,
                UnitPrice = product.UnitPrice,
                Quantity = quantity,
                ImageUrl = product.ImageUrl,
                MaxStock = product.StockQuantity
            });
        }

        await SaveCart();
        NotifyStateChanged();
    }

    // NUEVO MÉTODO: Actualizar cantidad directa (desde el carrito)
    public async Task UpdateQuantity(Guid productId, int quantity)
    {
        var item = _cart.FirstOrDefault(x => x.ProductId == productId);
        if (item != null)
        {
            if (quantity > 0)
            {
                // Validar que no supere el stock real
                if (quantity <= item.MaxStock)
                {
                    item.Quantity = quantity;
                }
                else
                {
                    item.Quantity = item.MaxStock; // Tope máximo
                }
            }
            else
            {
                // Si pone 0 o menos, podríamos eliminarlo, o dejarlo en 1. Decisión de diseño.
                // Aquí lo dejamos en 1.
                item.Quantity = 1;
            }

            await SaveCart();
            NotifyStateChanged();
        }
    }

    public async Task RemoveFromCart(Guid productId)
    {
        var item = _cart.FirstOrDefault(x => x.ProductId == productId);
        if (item != null)
        {
            _cart.Remove(item);
            await SaveCart();
            NotifyStateChanged();
        }
    }

    public async Task ClearCart()
    {
        _cart.Clear();
        await _localStorage.RemoveItemAsync(_storageKey);
        NotifyStateChanged();
    }

    private async Task SaveCart()
    {
        await _localStorage.SetItemAsync(_storageKey, _cart);
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
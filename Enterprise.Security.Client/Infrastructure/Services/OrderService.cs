using Enterprise.Security.Client.Core.Common;
using Enterprise.Security.Client.Core.DTOs.Orders;
using Enterprise.Security.Client.Core.Interfaces;
using System.Net.Http.Json;

namespace Enterprise.Security.Client.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<Guid>> CreateOrderAsync(CreateOrderDto dto)
        {
            var result = await _httpClient.PostAsJsonAsync("api/orders", dto);
            return (await result.Content.ReadFromJsonAsync<ApiResponse<Guid>>())!;
        }

        public async Task<ApiResponse<List<OrderResponseDto>>> GetMyOrdersAsync()
        {
            // Asumiendo que tu Backend tiene este endpoint (estándar en Clean Architecture)
            var result = await _httpClient.GetFromJsonAsync<ApiResponse<List<OrderResponseDto>>>("api/orders/my-orders");
            return result!;
        }

        public async Task<ApiResponse<OrderResponseDto>> GetOrderByIdAsync(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<ApiResponse<OrderResponseDto>>($"api/orders/{id}");
            return result!;
        }
        public async Task<ApiResponse<List<OrderResponseDto>>> GetAllOrdersAsync()
        {
            // Asumiendo que tienes GET api/orders (para admins)
            var result = await _httpClient.GetFromJsonAsync<ApiResponse<List<OrderResponseDto>>>("api/orders");
            return result!;
        }

        public async Task<ApiResponse<string>> ApproveOrderAsync(Guid id)
        {
            // Llamamos al endpoint de aprobación
            var result = await _httpClient.PutAsJsonAsync($"api/orders/{id}/approve", new { });
            return (await result.Content.ReadFromJsonAsync<ApiResponse<string>>())!;
        }
    }
}

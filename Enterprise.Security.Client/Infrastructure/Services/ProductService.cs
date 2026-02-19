using Enterprise.Security.Client.Core.Common;
using Enterprise.Security.Client.Core.DTOs.Inventory.Products;
using Enterprise.Security.Client.Core.Interfaces;
using System.Net.Http.Json;

namespace Enterprise.Security.Client.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<ProductDto>>> GetAllAsync()
        {
            return (await _httpClient.GetFromJsonAsync<ApiResponse<List<ProductDto>>>("api/products"))!;
        }

        public async Task<ApiResponse<ProductDto>> GetByIdAsync(Guid id)
        {
            return (await _httpClient.GetFromJsonAsync<ApiResponse<ProductDto>>($"api/products/{id}"))!;
        }

        public async Task<ApiResponse<Guid>> CreateAsync(CreateProductDto dto)
        {
            var result = await _httpClient.PostAsJsonAsync("api/products", dto);
            return (await result.Content.ReadFromJsonAsync<ApiResponse<Guid>>())!;
        }

        public async Task<ApiResponse<string>> UpdateAsync(UpdateProductDto dto)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/products/{dto.Id}", dto);
            return (await result.Content.ReadFromJsonAsync<ApiResponse<string>>())!;
        }

        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"api/products/{id}");
            return (await result.Content.ReadFromJsonAsync<ApiResponse<string>>())!;
        }

        public async Task<ApiResponse<string>> ToggleStatusAsync(Guid id)
        {
            var result = await _httpClient.PutAsync($"api/products/{id}/toggle-status", null);
            return (await result.Content.ReadFromJsonAsync<ApiResponse<string>>())!;
        }

        public async Task<ApiResponse<string>> AdjustStockAsync(AdjustStockDto dto)
        {
            var result = await _httpClient.PutAsJsonAsync("api/products/adjust-stock", dto);
            return (await result.Content.ReadFromJsonAsync<ApiResponse<string>>())!;
        }
    }
}

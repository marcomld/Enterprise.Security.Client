using Enterprise.Security.Client.Core.Common;
using Enterprise.Security.Client.Core.DTOs.Inventory.Categories;
using Enterprise.Security.Client.Core.Interfaces;
using System.Net.Http.Json;

namespace Enterprise.Security.Client.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<CategoryDto>>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<CategoryDto>>>("api/categories");
            return response!;
        }

        public async Task<ApiResponse<CategoryDto>> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<CategoryDto>>($"api/categories/{id}");
            return response!;
        }

        public async Task<ApiResponse<Guid>> CreateAsync(CreateCategoryDto dto)
        {
            var result = await _httpClient.PostAsJsonAsync("api/categories", dto);
            return (await result.Content.ReadFromJsonAsync<ApiResponse<Guid>>())!;
        }

        public async Task<ApiResponse<string>> UpdateAsync(UpdateCategoryDto dto)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/categories/{dto.Id}", dto);
            return (await result.Content.ReadFromJsonAsync<ApiResponse<string>>())!;
        }

        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"api/categories/{id}");
            return (await result.Content.ReadFromJsonAsync<ApiResponse<string>>())!;
        }

        public async Task<ApiResponse<string>> ToggleStatusAsync(Guid id)
        {
            var result = await _httpClient.PutAsync($"api/categories/{id}/toggle-status", null);
            return (await result.Content.ReadFromJsonAsync<ApiResponse<string>>())!;
        }
    }
}

using Enterprise.Security.Client.Core.Common;
using Enterprise.Security.Client.Core.DTOs.Invoicing;
using Enterprise.Security.Client.Core.Interfaces;
using System.Net.Http.Json;

namespace Enterprise.Security.Client.Infrastructure.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly HttpClient _httpClient;

        public InvoiceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<InvoiceResponseDto>>> GetMyInvoicesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<InvoiceResponseDto>>>("api/invoices/my-invoices");
            return response!;
        }

        public async Task<ApiResponse<List<InvoiceResponseDto>>> GetAllInvoicesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<InvoiceResponseDto>>>("api/invoices");
            return response!;
        }

        public async Task<ApiResponse<InvoiceResponseDto>> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<InvoiceResponseDto>>($"api/invoices/{id}");
            return response!;
        }

        public async Task<ApiResponse<Guid>> CreateDirectInvoiceAsync(CreateDirectInvoiceDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/invoices/direct", dto);
            return (await response.Content.ReadFromJsonAsync<ApiResponse<Guid>>())!;
        }
    }
}

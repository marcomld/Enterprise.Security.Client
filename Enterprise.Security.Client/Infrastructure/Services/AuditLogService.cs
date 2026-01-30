using Enterprise.Security.Client.Core.Common;
using Enterprise.Security.Client.Core.DTOs.Audit;
using Enterprise.Security.Client.Core.Interfaces;
using System.Net.Http.Json;

namespace Enterprise.Security.Client.Infrastructure.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly HttpClient _http;
        public AuditLogService(HttpClient http) => _http = http;

        public async Task<List<AuditLogResponseDto>> GetLogsAsync(string? search = null)
        {
            var url = "api/audit";
            if (!string.IsNullOrEmpty(search))
            {
                url += $"?search={Uri.EscapeDataString(search)}";
            }

            var response = await _http.GetFromJsonAsync<ApiResponse<List<AuditLogResponseDto>>>(url);
            return response?.Data ?? new List<AuditLogResponseDto>();
        }
    }
}

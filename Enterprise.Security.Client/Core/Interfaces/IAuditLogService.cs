using Enterprise.Security.Client.Core.DTOs.Audit;

namespace Enterprise.Security.Client.Core.Interfaces
{
    public interface IAuditLogService
    {
        Task<List<AuditLogResponseDto>> GetLogsAsync(string? search = null);
    }
}

namespace Enterprise.Security.Client.Core.DTOs.Audit
{
    public class AuditLogResponseDto
    {
        public Guid Id { get; set; }

        //Importante: Guid nullable para coincidir con tu Backend
        public Guid? UserId { get; set; }
        public string? UserEmail { get; set; }

        public string Action { get; set; } = string.Empty;
        public string Entity { get; set; } = string.Empty;
        public string? EntityId { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? AdditionalData { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

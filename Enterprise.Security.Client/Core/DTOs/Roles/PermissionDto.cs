namespace Enterprise.Security.Client.Core.DTOs.Roles
{
    public class PermissionDto
    {
        public string PermissionId { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsSelected { get; set; } // Mutable para el checkbox
    }
}

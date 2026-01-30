namespace Enterprise.Security.Client.Core.DTOs.Roles
{
    public class UpdateRolePermissionsDto
    {
        public string RoleId { get; set; } = string.Empty;
        public List<string> PermissionIds { get; set; } = new();
    }
}

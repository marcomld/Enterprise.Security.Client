namespace Enterprise.Security.Client.Core.DTOs.Roles
{
    public class AssignRoleDto
    {
        public Guid UserId { get; set; }
        public string RoleName { get; set; } = string.Empty;

        // Constructor vacío requerido por serializadores
        public AssignRoleDto() { }

        // Constructor de conveniencia para instanciar rápido en el código
        public AssignRoleDto(Guid userId, string roleName)
        {
            UserId = userId;
            RoleName = roleName;
        }
    }
}

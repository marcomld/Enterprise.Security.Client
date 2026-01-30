using Enterprise.Security.Client.Core.DTOs.Roles;

namespace Enterprise.Security.Client.Core.Interfaces
{
    public interface IRoleService
    {
        // Listar todos los roles
        Task<List<RoleResponseDto>> GetAllAsync();

        // Crear un nuevo rol
        Task<string> CreateAsync(CreateRoleDto role);

        // Borrar un rol
        Task<string> DeleteAsync(string roleId);

        // Asignar rol a usuario
        Task<string> AssignAsync(AssignRoleDto dto);

        // Quitar rol a usuario
        Task<string> UnassignAsync(AssignRoleDto dto);


        Task<List<PermissionDto>> GetPermissionsAsync(string roleId);
        Task<string> UpdatePermissionsAsync(UpdateRolePermissionsDto dto);
    }
}

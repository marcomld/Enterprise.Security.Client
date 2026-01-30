using Enterprise.Security.Client.Core.DTOs.Users;

namespace Enterprise.Security.Client.Core.Interfaces
{
    public interface IUserService
    {
        // Obtiene la lista enriquecida (con Nombres, Roles, etc.)
        Task<List<UserResponseDto>> GetAllAsync();

        // Acciones administrativas
        Task<string> ActivateAsync(Guid id);
        Task<string> DeactivateAsync(Guid id);
        Task<string> CreateAsync(CreateUserDto user);
    }
}

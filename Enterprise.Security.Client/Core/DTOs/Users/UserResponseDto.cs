namespace Enterprise.Security.Client.Core.DTOs.Users
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;  // Nuevo
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty; // Nuevo
        public string LastName { get; set; } = string.Empty;  // Nuevo
        public bool IsActive { get; set; }

        // Propiedad calculada útil para mostrar en tablas
        public string FullName => $"{FirstName} {LastName}";

        // NUEVO: Inicializamos la lista para evitar NullReferenceException
        public List<string> Roles { get; set; } = new();

        // Helper: Propiedad calculada para mostrar roles en texto (opcional)
        public string RoleNames => string.Join(", ", Roles);
    }
}

using System.ComponentModel.DataAnnotations;

namespace Enterprise.Security.Client.Core.DTOs.Roles
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 letras.")]
        public string RoleName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Description { get; set; } = string.Empty;
    }
}

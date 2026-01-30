using Enterprise.Security.Client.Core.Common;
using Enterprise.Security.Client.Core.DTOs.Roles;
using Enterprise.Security.Client.Core.Interfaces;
using System.Net.Http.Json;

namespace Enterprise.Security.Client.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _http;

        public RoleService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<RoleResponseDto>> GetAllAsync()
        {
            try
            {
                var response = await _http.GetFromJsonAsync<ApiResponse<List<RoleResponseDto>>>("api/roles");

                if (response != null && response.Success && response.Data != null)
                {
                    return response.Data;
                }

                return new List<RoleResponseDto>();
            }
            catch (Exception)
            {
                // En caso de error de red, retornamos lista vacía o podrías manejarlo diferente
                return new List<RoleResponseDto>();
            }
        }

        public async Task<string> CreateAsync(CreateRoleDto role)
        {
            // POST api/roles
            var response = await _http.PostAsJsonAsync("api/roles", role);
            return await HandleResult(response);
        }

        public async Task<string> DeleteAsync(string roleId)
        {
            // DELETE api/roles/{id}
            var response = await _http.DeleteAsync($"api/roles/{roleId}");
            return await HandleResult(response);
        }

        public async Task<string> AssignAsync(AssignRoleDto dto)
        {
            // POST api/roles/assign
            var response = await _http.PostAsJsonAsync("api/roles/assign", dto);
            return await HandleResult(response);
        }

        public async Task<string> UnassignAsync(AssignRoleDto dto)
        {
            // POST api/roles/unassign
            var response = await _http.PostAsJsonAsync("api/roles/unassign", dto);
            return await HandleResult(response);
        }

        // --- Helper para leer tu ApiResponse estándar ---
        private async Task<string> HandleResult(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                return "Error de conexión con el servidor.";

            try
            {
                // Leemos el wrapper ApiResponse<T> (usamos string porque el Data suele ser un mensaje simple)
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();

                // Si Success es true, retornamos vacío (indicando éxito total)
                if (result != null && result.Success)
                    return string.Empty;

                // Si falló (ej: "Rol ya existe"), retornamos el mensaje de error del backend
                return result?.Error ?? "Error desconocido en la operación.";
            }
            catch
            {
                return "Error al procesar la respuesta del servidor.";
            }
        }



        public async Task<List<PermissionDto>> GetPermissionsAsync(string roleId)
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<List<PermissionDto>>>($"api/roles/{roleId}/permissions");
            return response?.Data ?? new List<PermissionDto>();
        }

        public async Task<string> UpdatePermissionsAsync(UpdateRolePermissionsDto dto)
        {
            var response = await _http.PutAsJsonAsync("api/roles/permissions", dto);
            return await HandleResult(response);
        }
    }
}

using Enterprise.Security.Client.Core.Common;
using Enterprise.Security.Client.Core.DTOs.Users;
using Enterprise.Security.Client.Core.Interfaces;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Enterprise.Security.Client.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<UserResponseDto>> GetAllAsync()
        {
            try
            {
                // Blazor deserializará automáticamente el JSON con FirstName, LastName, etc.
                // que agregaste al DTO.
                var response = await _http.GetFromJsonAsync<List<UserResponseDto>>("api/users");
                return response ?? new List<UserResponseDto>();
            }
            catch (Exception)
            {
                // En producción podrías loguear esto o mostrar un Toast
                return new List<UserResponseDto>();
            }
        }

        public async Task<string> ActivateAsync(Guid id)
        {
            var response = await _http.PutAsync($"api/users/{id}/activate", null);
            return await HandleResult(response);
        }

        public async Task<string> DeactivateAsync(Guid id)
        {
            var response = await _http.PutAsync($"api/users/{id}/deactivate", null);
            return await HandleResult(response);
        }

        public async Task<string> CreateAsync(CreateUserDto user)
        {
            // PostAsJsonAsync serializará tu clase a un JSON que encajará 
            // perfectamente en el record del backend.
            var response = await _http.PostAsJsonAsync("api/users", user);
            return await HandleResult(response);
        }


        // --- Helper para leer tu estándar ApiResponse ---
        private async Task<string> HandleResult(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                return "Error de conexión con el servidor.";

            try
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();

                // Si Success es true, retornamos vacío (sin error)
                if (result != null && result.Success)
                    return string.Empty;

                // Si falló, retornamos el mensaje de error del backend
                return result?.Error ?? "Error desconocido en la operación.";
            }
            catch
            {
                return "Error al procesar la respuesta del servidor.";
            }
        }
    }
}

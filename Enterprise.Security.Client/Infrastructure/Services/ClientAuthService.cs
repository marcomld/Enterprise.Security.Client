using Enterprise.Security.Client.Core.Common;
using Enterprise.Security.Client.Core.DTOs.Auth;
using Enterprise.Security.Client.Core.Interfaces;
using Enterprise.Security.Client.Infrastructure.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace Enterprise.Security.Client.Infrastructure.Services
{
    public class ClientAuthService : IClientAuthService
    {
        private readonly HttpClient _http;
        private readonly CustomAuthenticationStateProvider _authStateProvider;

        public ClientAuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
        }

        public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", request);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponseDto>>();

            if (result != null && result.Success && result.Data != null)
            {
                await _authStateProvider.LoginAsync(result.Data);
            }

            return result!;
        }

        public async Task<ApiResponse<LoginResponseDto>> RegisterAsync(RegisterRequestDto request)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", request);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponseDto>>();

            // Opcional: Si quieres que el registro loguee automáticamente al usuario
            if (result != null && result.Success && result.Data != null)
            {
                await _authStateProvider.LoginAsync(result.Data);
            }

            return result!;
        }

        public async Task LogoutAsync()
        {
            await _authStateProvider.LogoutAsync();
        }
    }
}

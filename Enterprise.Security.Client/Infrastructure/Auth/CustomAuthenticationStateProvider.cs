using System.Net.Http.Headers;
using System.Net.Http.Json; // Necesario para PostAsJsonAsync
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Enterprise.Security.Client.Core.DTOs.Auth; // Asegura tus DTOs
using Enterprise.Security.Client.Core.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace Enterprise.Security.Client.Infrastructure.Auth;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;
    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    private const string AccessTokenKey = "accessToken";
    private const string RefreshTokenKey = "refreshToken"; // 👈 Nueva constante

    public CustomAuthenticationStateProvider(ILocalStorageService localStorage, HttpClient httpClient)
    {
        _localStorage = localStorage;
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>(AccessTokenKey);

        if (string.IsNullOrEmpty(token) || !_tokenHandler.CanReadToken(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var jwtToken = _tokenHandler.ReadJwtToken(token);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var identity = new ClaimsIdentity(jwtToken.Claims, "jwt", "unique_name", "role");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    // 👇 CAMBIO 1: Recibimos el objeto completo, no solo el string
    public async Task LoginAsync(LoginResponseDto response)
    {
        await _localStorage.SetItemAsync(AccessTokenKey, response.AccessToken);
        await _localStorage.SetItemAsync(RefreshTokenKey, response.RefreshToken); // 👈 Guardamos el Refresh

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    // 👇 CAMBIO 2: Logout real conectado a la API
    public async Task LogoutAsync()
    {
        try
        {
            // 1. Recuperar el Refresh Token para enviarlo al backend
            var refreshToken = await _localStorage.GetItemAsync<string>(RefreshTokenKey);

            if (!string.IsNullOrEmpty(refreshToken))
            {
                // 2. Llamar al endpoint de Logout del Backend
                var request = new LogoutRequestDto(refreshToken);
                await _httpClient.PostAsJsonAsync("api/auth/logout", request);
            }
        }
        catch (Exception)
        {
            // Si falla la conexión (servidor caído), igual cerramos sesión localmente
            // para no dejar al usuario atrapado.
        }
        finally
        {
            // 3. Limpieza local (Siempre se ejecuta)
            await _localStorage.RemoveItemAsync(AccessTokenKey);
            await _localStorage.RemoveItemAsync(RefreshTokenKey);
            _httpClient.DefaultRequestHeaders.Authorization = null;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
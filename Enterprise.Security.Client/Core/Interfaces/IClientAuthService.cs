using Enterprise.Security.Client.Core.Common;
using Enterprise.Security.Client.Core.DTOs.Auth;

namespace Enterprise.Security.Client.Core.Interfaces
{
    public interface IClientAuthService
    {
        Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request);
        Task<ApiResponse<LoginResponseDto>> RegisterAsync(RegisterRequestDto request);
        Task LogoutAsync();
    }
}

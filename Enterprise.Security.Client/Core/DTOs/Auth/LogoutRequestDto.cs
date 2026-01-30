namespace Enterprise.Security.Client.Core.DTOs.Auth
{
    public class LogoutRequestDto
    {
        public string RefreshToken { get; set; } = string.Empty;
        public LogoutRequestDto() { }

        public LogoutRequestDto(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}

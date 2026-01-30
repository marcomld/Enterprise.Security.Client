namespace Enterprise.Security.Client.Core.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }

        // Helper para verificar si fallo rapido
        public bool IsSuccess => Success;
    }
}

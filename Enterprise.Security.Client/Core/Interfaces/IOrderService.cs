using Enterprise.Security.Client.Core.Common;
using Enterprise.Security.Client.Core.DTOs.Orders;

namespace Enterprise.Security.Client.Core.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResponse<Guid>> CreateOrderAsync(CreateOrderDto dto);
        Task<ApiResponse<List<OrderResponseDto>>> GetMyOrdersAsync();
        Task<ApiResponse<OrderResponseDto>> GetOrderByIdAsync(Guid id);
        Task<ApiResponse<List<OrderResponseDto>>> GetAllOrdersAsync();
        Task<ApiResponse<string>> ApproveOrderAsync(Guid id);
    }
}

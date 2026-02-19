using Enterprise.Security.Client.Core.Common;
using Enterprise.Security.Client.Core.DTOs.Invoicing;

namespace Enterprise.Security.Client.Core.Interfaces
{
    public interface IInvoiceService
    {
        // Para el Cliente (ver sus propias facturas)
        Task<ApiResponse<List<InvoiceResponseDto>>> GetMyInvoicesAsync();

        // Para el Supervisor/Admin (ver todas)
        Task<ApiResponse<List<InvoiceResponseDto>>> GetAllInvoicesAsync();

        // Para ver el detalle de una factura específica
        Task<ApiResponse<InvoiceResponseDto>> GetByIdAsync(Guid id);

        // Para el Vendedor (crear factura directa sin pedido previo)
        Task<ApiResponse<Guid>> CreateDirectInvoiceAsync(CreateDirectInvoiceDto dto);
    }
}

using Enterprise.Security.Client.Core.Common;
using Enterprise.Security.Client.Core.DTOs.Inventory.Categories;
using Enterprise.Security.Client.Core.DTOs.Inventory.Products;

namespace Enterprise.Security.Client.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResponse<List<CategoryDto>>> GetAllAsync();
        Task<ApiResponse<CategoryDto>> GetByIdAsync(Guid id);
        Task<ApiResponse<Guid>> CreateAsync(CreateCategoryDto dto);
        Task<ApiResponse<string>> UpdateAsync(UpdateCategoryDto dto);
        Task<ApiResponse<string>> DeleteAsync(Guid id);
        Task<ApiResponse<string>> ToggleStatusAsync(Guid id);
    }

    public interface IProductService
    {
        Task<ApiResponse<List<ProductDto>>> GetAllAsync();
        Task<ApiResponse<ProductDto>> GetByIdAsync(Guid id);
        Task<ApiResponse<Guid>> CreateAsync(CreateProductDto dto);
        Task<ApiResponse<string>> UpdateAsync(UpdateProductDto dto);
        Task<ApiResponse<string>> DeleteAsync(Guid id);
        Task<ApiResponse<string>> ToggleStatusAsync(Guid id);
        Task<ApiResponse<string>> AdjustStockAsync(AdjustStockDto dto);
    }
}

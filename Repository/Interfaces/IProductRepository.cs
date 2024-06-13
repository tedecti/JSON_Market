using JSON_Market.Models.Product;
using JSON_Market.Models.Product.GET;
using JSON_Market.Models.Product.POST_PUT;

namespace JSON_Market.Repository.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllProductsAsync();
    Task<GetAllProductsBySellerDto> GetAllProductsBySellerAsync(Guid sellerId);
    Task<Product> GetProductByIdAsync(Guid id);
    Task<List<Product>> GetAllProductsByIdsAsync(List<Guid> productIds);
    Task<Product> CreateProductAsync(Guid sellerId, CreateOrEditProductDto createOrEditProductDto);
    Task<Product> EditProductAsync(Guid productId, CreateOrEditProductDto createOrEditProductDto);
    Task<bool> RemoveProductAsync(Guid productId);
}
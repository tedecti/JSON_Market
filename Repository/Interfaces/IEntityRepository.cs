using JSON_Market.Models;
using JSON_Market.Models.Customer;
using JSON_Market.Models.Customer.GET;
using JSON_Market.Models.Order;
using JSON_Market.Models.Order.GET;
using JSON_Market.Models.Product;
using JSON_Market.Models.Product.GET;
using JSON_Market.Models.Seller;
using JSON_Market.Models.Seller.GET;

namespace JSON_Market.Repository.Interfaces;

public interface IEntityRepository
{
    Task<SellerDto> GetSellerByIdAsync(Guid id);
    Task<IEnumerable<SellerDto>> GetAllSellersAsync();
    Task<IEnumerable<ShortProductDto>> GetCreatedProductsBySellerAsync(Guid sellerId);
    Task<CustomerDto> GetCustomerByIdAsync(Guid id);
    Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
    Task<IEnumerable<ShortOrderDto>?> GetOrderHistoryByCustomerAsync(Guid customerId);
}
using JSON_Market.Models.Order;
using JSON_Market.Models.Order.GET;

namespace JSON_Market.Repository.Interfaces;

public interface IOrderRepository
{
    Task<GetAllOrdersByCustomerDto> GetAllOrdersByCustomerAsync(Guid customerId);
    Task<Order> GetOrderByIdAsync(Guid id);
    Task<GetAllOrdersByCustomerDto> GetOrderByIdForResponseAsync(Guid id);
    Task<Order> CreateOrderAsync(Guid customerId, List<Guid> productIds);
    Task<Order> EditOrderAsync(Guid orderId, List<Guid> productIds);
    Task<bool> RemoveOrderAsync(Guid orderId);
}
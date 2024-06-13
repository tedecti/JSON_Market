using JSON_Market.Models.Order;

namespace JSON_Market.Repository.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetAllOrdersByCustomerAsync(Guid customerId);
    Task<Order> GetOrderByIdAsync(Guid id);
    Task<Order> CreateOrderAsync(Guid customerId, List<Guid> productIds);
    Task<Order> EditOrderAsync(Guid orderId, List<Guid> productIds);
    Task<bool> RemoveOrderAsync(Guid orderId);
}
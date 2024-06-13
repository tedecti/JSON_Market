using JSON_Market.Data;
using JSON_Market.Models.Order;
using JSON_Market.Models.Product;
using JSON_Market.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace JSON_Market.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    private readonly IProductRepository _productRepository;

    public OrderRepository(AppDbContext context, IProductRepository productRepository)
    {
        _context = context;
        _productRepository = productRepository;
    }

    public async Task<List<Order>> GetAllOrdersByCustomerAsync(Guid customerId)
    {
        var orders = await _context.Orders
            .Where(o => o.CustomerId == customerId)
            .Include(o => o.Customer.Name)
            .ToListAsync();
        return orders;
    }

    public async Task<Order> GetOrderByIdAsync(Guid id)
    {
        var order = await _context.Orders
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        return order;
    }

    public async Task<Order> CreateOrderAsync(Guid customerId, List<Guid> productIds)
    {
        var products = await _productRepository.GetAllProductsByIdsAsync(productIds);
        if (products.Count != productIds.Count)
        {
            throw new ArgumentException("Некоторые из переданных продуктов не найдены.");
        }

        var newOrder = new Order()
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            Products = products
        };
        _context.Orders.Add(newOrder);
        await _context.SaveChangesAsync();

        return newOrder;
    }

    public async Task<Order> EditOrderAsync(Guid orderId, List<Guid> productIds)
    {
        var products = await _productRepository.GetAllProductsByIdsAsync(productIds);
        if (products.Count != productIds.Count)
        {
            throw new ArgumentException("Некоторые из переданных продуктов не найдены.");
        }

        var existingOrder = await GetOrderByIdAsync(orderId);
        if (existingOrder == null)
        {
            throw new ArgumentException("Заказ не найден.");
        }

        var productsToRemove = existingOrder.Products
            .Where(p => !products.Select(np => np.Id).Contains(p.Id))
            .ToList();
        var existingProductIds = existingOrder.Products.Select(p => p.Id).ToList();
        var productsToAdd = products
            .Where(p => !existingProductIds.Contains(p.Id))
            .ToList();
        await _context.SaveChangesAsync();
        return existingOrder;
    }

    public async Task<bool> RemoveOrderAsync(Guid orderId)
    {
        var existingOrder = await GetOrderByIdAsync(orderId);
        _context.Orders.Remove(existingOrder);
        await _context.SaveChangesAsync();
        return existingOrder == null;
    }
}
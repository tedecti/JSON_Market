using JSON_Market.Data;
using JSON_Market.Models;
using JSON_Market.Models.Order;
using JSON_Market.Models.Order.GET;
using JSON_Market.Models.Product;
using JSON_Market.Models.Product.GET;
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

    public async Task<GetAllOrdersByCustomerDto> GetAllOrdersByCustomerAsync(Guid customerId)
    {
        var order = await _context.Orders
            .Where(p => p.CustomerId == customerId)
            .Include(o => o.Customer)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync();
        var orderDto = new GetAllOrdersByCustomerDto()
        {
            Name = order.Customer?.Name,
            Orders = order.OrderProducts.Select(op => new GetAllProductsDto
            {
                Title = op.Product.Title,
                Description = op.Product.Description,
                Price = op.Product.Price,
                Discount = op.Product.Discount,
                ImageUrls = op.Product.ImageUrls,
                CreatedAt = op.Product.CreatedAt
            }).ToList()
        };
        return orderDto;
    }

    public async Task<Order> GetOrderByIdAsync(Guid id)
    {
        var order = await _context.Orders
            .Where(p => p.Id == id)
            .Include(o => o.Customer)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync();
        return order;
    }

    public async Task<GetAllOrdersByCustomerDto> GetOrderByIdForResponseAsync(Guid id)
    {
        var order = await _context.Orders
            .Where(p => p.Id == id)
            .Include(o => o.Customer)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync();
        var orderDto = new GetAllOrdersByCustomerDto()
        {
            Name = order.Customer?.Name,
            Orders = order.OrderProducts.Select(op => new GetAllProductsDto
            {
                Title = op.Product.Title,
                Description = op.Product.Description,
                Price = op.Product.Price,
                Discount = op.Product.Discount,
                ImageUrls = op.Product.ImageUrls,
                CreatedAt = op.Product.CreatedAt
            }).ToList()
        };
        return orderDto;
    }

    public async Task<Order> CreateOrderAsync(Guid customerId, List<Guid> productIds)
    {
        var products = await _productRepository.GetAllProductsByIdsAsync(productIds);
        if (products.Count != productIds.Count)
        {
            throw new ArgumentException("Некоторые из переданных продуктов не найдены.");
        }

        var newId = Guid.NewGuid();
        var newOrder = new Order()
        {
            Id = newId,
            CustomerId = customerId,
            OrderProducts = products.Select(p => new OrderProduct
            {
                OrderId = newId,
                ProductId = p.Id
            }).ToList()
        };

        var customer = await _context.Customers.FindAsync(customerId);
        customer.OrderHistory.Add(newOrder);

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

        var existingOrderProducts = await _context.OrderProducts
            .Where(op => op.OrderId == orderId)
            .ToListAsync();
        _context.OrderProducts.RemoveRange(existingOrderProducts);

        var newOrderProducts = products.Select(p => new OrderProduct
        {
            OrderId = orderId,
            ProductId = p.Id
        }).ToList();
        _context.OrderProducts.AddRange(newOrderProducts);

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
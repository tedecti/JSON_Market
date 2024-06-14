using JSON_Market.Data;
using JSON_Market.Models;
using JSON_Market.Models.Customer;
using JSON_Market.Models.Customer.GET;
using JSON_Market.Models.Order;
using JSON_Market.Models.Order.GET;
using JSON_Market.Models.Product;
using JSON_Market.Models.Product.GET;
using JSON_Market.Models.Seller;
using JSON_Market.Models.Seller.GET;
using JSON_Market.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JSON_Market.Repository;

public class EntityRepository : IEntityRepository
{
    private readonly AppDbContext _context;

    public EntityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<SellerDto> GetSellerByIdAsync(Guid id)
    {
        var seller = await _context.Sellers
            .Include(s => s.CreatedProducts)
            .FirstOrDefaultAsync(s => s.Id == id);
        return MapToSellerDTO(seller);
    }

    public async Task<IEnumerable<SellerDto>> GetAllSellersAsync()
    {
        var sellers = await _context.Sellers
            .Include(s => s.CreatedProducts)
            .ToListAsync();
        return sellers.Select(MapToSellerDTO);
    }

    public async Task<IEnumerable<ShortProductDto>> GetCreatedProductsBySellerAsync(Guid sellerId)
    {
        var seller = await _context.Sellers
            .Include(s => s.CreatedProducts)
            .FirstOrDefaultAsync(s => s.Id == sellerId);

        return seller.CreatedProducts.Select(MapToProductDTO);
    }

    public async Task<CustomerDto> GetCustomerByIdAsync(Guid id)
    {
        var customer = await _context.Customers
            .Include(c => c.OrderHistory)
            .ThenInclude(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync(c => c.Id == id);
        return MapToCustomerDTO(customer);
    }

    public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
    {
        var customers = await _context.Customers
            .Include(c => c.OrderHistory)
            .ThenInclude(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .ToListAsync();
        return customers.Select(MapToCustomerDTO);
    }

    public async Task<IEnumerable<ShortOrderDto>?> GetOrderHistoryByCustomerAsync(Guid customerId)
    {
        var customer = await _context.Customers
            .Include(c => c.OrderHistory)
            .ThenInclude(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync(c => c.Id == customerId);

        return customer?.OrderHistory.Select(MapToOrderDTO);
    }

    private SellerDto MapToSellerDTO(Seller seller)
    {
        if (seller == null) return null;

        return new SellerDto
        {
            Name = seller.Name,
            CreatedProducts = (ICollection<ShortProductDto>)seller.CreatedProducts.Select(MapToProductDTO).ToList()
        };
    }

    private ShortProductDto MapToProductDTO(Product product)
    {
        if (product == null) return null;

        return new ShortProductDto
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Discount = product.Discount,
            Price = product.Price
        };
    }

    private CustomerDto MapToCustomerDTO(Customer customer)
    {
        if (customer == null) return null;

        return new CustomerDto
        {
            Name = customer.Name,
            OrderHistory = (ICollection<ShortOrderDto>)customer.OrderHistory.Select(MapToOrderDTO).ToList()
        };
    }

    private ShortOrderDto MapToOrderDTO(Order order)
    {
        if (order == null) return null;
        return new ShortOrderDto
        {
            OrderProducts = (ICollection<OrderProductDto>)order.OrderProducts.Select(MapToOrderProductDTO).ToList()
        };
    }

    private OrderProductDto MapToOrderProductDTO(OrderProduct orderProduct)
    {
        if (orderProduct == null) return null;

        return new OrderProductDto
        {
            OrderId = orderProduct.OrderId,
            ProductId = orderProduct.ProductId,
            Product = MapToProductDTO(orderProduct.Product)
        };
    }
}
using JSON_Market.Models;
using JSON_Market.Models.Customer;
using JSON_Market.Models.Order;
using JSON_Market.Models.Product;
using JSON_Market.Models.Seller;
using Microsoft.EntityFrameworkCore;

namespace JSON_Market.Data;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Npgsql"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureHasManyWithOne<Customer, Order>(
            c => c.OrderHistory,
            o => o.Customer,
            o => o.CustomerId,
            DeleteBehavior.Cascade
        );

        modelBuilder.ConfigureHasManyWithOne<Seller, Product>(
            s => s.CreatedProducts,
            p => p.Seller,
            p => p.SellerId,
            DeleteBehavior.Cascade
        );

        modelBuilder.Entity<OrderProduct>()
            .HasKey(op => new { op.OrderId, op.ProductId });

        modelBuilder.ConfigureHasOneWithMany<Order, OrderProduct>(
            op => op.Order,
            o => o.OrderProducts,
            op => op.OrderId,
            DeleteBehavior.Cascade
        );

        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany()
            .HasForeignKey(op => op.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<OrderProduct> OrderProducts { get; set; } = default!;
    public DbSet<Seller> Sellers { get; set; } = default!;
}
using JSON_Market.Models;
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
        modelBuilder.ConfigureHasManyWithOne<Customer, Order>(c => c.OrderHistory,
            o => o.Customer,
            o => o.CustomerId,
            DeleteBehavior.Cascade);
        modelBuilder.ConfigureHasManyWithOne<Seller, Product>(s => s.CreatedProducts,
            p => p.Seller,
            p => p.SellerId,
            DeleteBehavior.Cascade);
        modelBuilder.Entity<Order>().HasMany(o => o.Products);
    }

    public DbSet<Customer> Customers;
    public DbSet<Order> Orders;
    public DbSet<Product> Products;
    public DbSet<Seller> Sellers;
}
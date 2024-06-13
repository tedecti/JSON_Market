using JSON_Market.Models;
using JSON_Market.Models.Order;
using JSON_Market.Models.Product;
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
        modelBuilder.Entity<Order>()
            .HasMany(o => o.Products)
            .WithMany();
        ;
    }

    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Seller> Sellers { get; set; } = default!;
}
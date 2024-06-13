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
        modelBuilder.Entity<Customer>()
            .HasMany(c=>c.OrderHistory)
            .WithOne(c=>c.Customer);
    }

    public DbSet<Customer> Customers;
}
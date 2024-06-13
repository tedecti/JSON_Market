using JSON_Market.Models;
using JSON_Market.Models.Product;

namespace JSON_Market.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();
        
        if (!context.Customers.Any() && !context.Sellers.Any())
        {
            var customers = new List<Customer>
            {
                new Customer { Id = Guid.NewGuid(), Name = "Anton" },
                new Customer { Id = Guid.NewGuid(), Name = "Kaplin" }
            };
            context.Customers.AddRange(customers);
            
            var sellers = new List<Seller>
            {
                new Seller { Id = Guid.NewGuid(), Name = "Artem"},
            };
            context.Sellers.AddRange(sellers);
            context.SaveChanges();
        }
    }
}
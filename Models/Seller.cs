namespace JSON_Market.Models;

public class Seller
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Order> CreatedOrders { get; set; } = [];
}
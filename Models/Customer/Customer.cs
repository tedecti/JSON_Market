namespace JSON_Market.Models.Customer;

public class Customer
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public List<Order.Order> OrderHistory { get; set; } = [];
}
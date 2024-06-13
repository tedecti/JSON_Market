namespace JSON_Market.Models;

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public ICollection<Product> Products { get; set; }
}
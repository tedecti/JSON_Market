namespace JSON_Market.Models.Order;

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public ICollection<Product.Product> Products { get; set; }
}
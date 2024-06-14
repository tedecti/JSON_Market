namespace JSON_Market.Models;

public class OrderProduct
{
    public Guid OrderId { get; set; }
    public Order.Order Order { get; set; }
    public Guid ProductId { get; set; }
    public Product.Product Product { get; set; }
}
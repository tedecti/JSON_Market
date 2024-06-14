using JSON_Market.Models.Product.GET;

namespace JSON_Market.Models;

public class OrderProductDto
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public ShortProductDto Product { get; set; }
}
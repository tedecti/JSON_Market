using JSON_Market.Models.Product.GET;

namespace JSON_Market.Models.Order.GET;

public class ShortOrderDto
{
    public ICollection<OrderProductDto> OrderProducts { get; set; } = new List<OrderProductDto>();
}
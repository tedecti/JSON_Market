namespace JSON_Market.Models.Order.GET;

public class GetAllOrdersDto
{
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
namespace JSON_Market.Models.Order.GET;

public class GetAllOrdersByCustomerDto
{
    public string? Name { get; set; }
    public Order Order { get; set; }
}
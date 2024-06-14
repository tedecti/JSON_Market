using JSON_Market.Models.Order.GET;

namespace JSON_Market.Models.Customer.GET;

public class CustomerDto
{
    public string? Name { get; set; }
    public ICollection<ShortOrderDto> OrderHistory { get; set; } = new List<ShortOrderDto>();
}
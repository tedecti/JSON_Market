using JSON_Market.Models.Product.GET;

namespace JSON_Market.Models.Order.GET;

public class GetAllOrdersByCustomerDto
{
    public string? Name { get; set; }
    public List<GetAllProductsDto> Orders { get; set; }
}
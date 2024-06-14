using JSON_Market.Models.Product.GET;

namespace JSON_Market.Models.Seller.GET;

public class SellerDto
{
    public string? Name { get; set; }
    public ICollection<ShortProductDto> CreatedProducts { get; set; } = new List<ShortProductDto>();
}
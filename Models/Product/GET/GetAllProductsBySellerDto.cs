namespace JSON_Market.Models.Product.GET;

public class GetAllProductsBySellerDto
{
    public string? Name { get; set; }
    public List<GetAllProductsDto> Products { get; set; } 
}
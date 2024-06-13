namespace JSON_Market.Models.Product.GET;

public class GetAllProductsDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public ushort Price { get; set; }
    public byte Discount { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<string> ImageUrls { get; set; } = [];
}
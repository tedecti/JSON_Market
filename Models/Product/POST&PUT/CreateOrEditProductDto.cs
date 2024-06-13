namespace JSON_Market.Models.Product.POST_PUT;

public class CreateOrEditProductDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public ushort Price { get; set; }
    public byte Discount { get; set; }
    public List<string> ImageUrls { get; set; } = [];
}
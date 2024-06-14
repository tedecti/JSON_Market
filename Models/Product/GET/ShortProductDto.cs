namespace JSON_Market.Models.Product.GET;

public class ShortProductDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }

    public decimal Price { get; set; }
    public byte Discount { get; set; }
}
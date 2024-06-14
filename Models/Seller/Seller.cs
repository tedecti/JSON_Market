namespace JSON_Market.Models.Seller;

public class Seller
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Product.Product> CreatedProducts { get; set; } = new List<Product.Product>();
}
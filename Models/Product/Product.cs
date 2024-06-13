    namespace JSON_Market.Models.Product;

    public class Product
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ushort Price { get; set; }
        public byte Discount { get; set; }
        public Guid SellerId { get; set; }
        public Seller? Seller { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string>? ImageUrls { get; set; } = [];
    }
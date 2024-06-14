using AutoMapper;
using JSON_Market.Data;
using JSON_Market.Models;
using JSON_Market.Models.Product;
using JSON_Market.Models.Product.GET;
using JSON_Market.Models.Product.POST_PUT;
using JSON_Market.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace JSON_Market.Repository;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileRepository _fileRepository;

    public ProductRepository(AppDbContext context, IMapper mapper, IFileRepository fileRepository)
    {
        _context = context;
        _mapper = mapper;
        _fileRepository = fileRepository;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var products = await _context.Products.ToListAsync();
        return products;
    }

    public async Task<GetAllProductsBySellerDto> GetAllProductsBySellerAsync(Guid sellerId)
    {
        var products = await _context.Products
            .Where(p => p.SellerId == sellerId)
            .Include(p => p.Seller)
            .ToListAsync();
        var result = new GetAllProductsBySellerDto
        {
            Name = products.FirstOrDefault()?.Seller?.Name,
            Products = _mapper.Map<List<GetAllProductsDto>>(products)
        };
        return result;
    }

    public async Task<Product> GetProductByIdAsync(Guid productId)
    {
        var product = await _context.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();
        return product;
    }

    public async Task<List<Product>> GetAllProductsByIdsAsync(List<Guid> productIds)
    {
        var products = await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();
        return products;
    }

    public async Task<Product> CreateProductAsync(Guid sellerId, CreateOrEditProductDto createOrEditProductDto)
    {
        var imgs = new List<string>();
        foreach (var file in createOrEditProductDto.ImageUrls)
        {
            imgs.Add(await _fileRepository.SaveFile(file));
        }
        var seller = await _context.Sellers.FindAsync(sellerId);
        if (seller == null)
        {
            throw new Exception("Продавец не найден");
        }
        var newProduct = new Product()
        {
            Id = Guid.NewGuid(),
            Title = createOrEditProductDto.Title,
            Description = createOrEditProductDto.Description,
            Price = createOrEditProductDto.Price,
            CreatedAt = DateTime.UtcNow,
            Discount = createOrEditProductDto.Discount,
            ImageUrls = imgs.ToList(),
            SellerId = sellerId
        };
        seller.CreatedProducts.Add(newProduct);
        _context.Products.Add(newProduct);
        await _context.SaveChangesAsync();
        return newProduct;
    }

    public async Task<Product> EditProductAsync(Guid productId, CreateOrEditProductDto createOrEditProductDto)
    {
        var existingProduct = await GetProductByIdAsync(productId);

        if (existingProduct == null)
        {
            throw new ArgumentException("Нет такого продукта");
            return null;
        }

        var productImages = existingProduct.ImageUrls;
        foreach (var img in productImages)
        {
            await _fileRepository.DeleteFileFromStorage(img);
        }

        var imgs = new List<string>();
        foreach (var file in createOrEditProductDto.ImageUrls)
        {
            imgs.Add(await _fileRepository.SaveFile(file));
        }

        existingProduct.Title = createOrEditProductDto.Title;
        existingProduct.Description = createOrEditProductDto.Description;
        existingProduct.Price = createOrEditProductDto.Price;
        existingProduct.Discount = createOrEditProductDto.Discount;
        existingProduct.ImageUrls = productImages;

        await _context.SaveChangesAsync();
        return existingProduct;
    }

    public async Task<bool> RemoveProductAsync(Guid productId)
    {
        var existingProduct = await GetProductByIdAsync(productId);
        _context.Products.Remove(existingProduct);
        await _context.SaveChangesAsync();
        return existingProduct == null;
    }
}
using AutoMapper;
using JSON_Market.Models.Order.GET;
using JSON_Market.Models.Product.GET;
using JSON_Market.Models.Product.POST_PUT;
using JSON_Market.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JSON_Market.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        [HttpGet("{sellerId}/seller")]
        public async Task<IActionResult> GetProductsBySeller(Guid sellerId)
        {
            var products = await _productRepository.GetAllProductsBySellerAsync(sellerId);
            var productsMap = _mapper.Map<GetAllProductsBySellerDto>(products);
            return Ok(productsMap);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductsById(Guid productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            var productMap = _mapper.Map<GetAllProductsDto>(product);
            return Ok(productMap);
        }

        [HttpPost()]
        public async Task<IActionResult> PostProduct([FromBody] CreateOrEditProductDto createProductDto, Guid sellerId)
        {
            var order = await _productRepository.CreateProductAsync(sellerId, createProductDto);
            return Created();
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> PutProduct([FromQuery] Guid productId, [FromBody] CreateOrEditProductDto editProductDto)
        {
            var order = await _productRepository.EditProductAsync(productId, editProductDto);
            return NoContent();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            var order = await _productRepository.RemoveProductAsync(productId);
            return NoContent();
        }
    }
}
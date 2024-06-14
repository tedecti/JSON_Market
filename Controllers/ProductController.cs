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
            if (products == null)
            {
                return NotFound();
            }
            var productsMap = _mapper.Map<GetAllProductsBySellerDto>(products);
            return Ok(productsMap);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductsById(Guid productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            var productMap = _mapper.Map<GetAllProductsDto>(product);
            return Ok(productMap);
        }

        [HttpPost("{sellerId}")]
        public async Task<IActionResult> PostProduct([FromForm] CreateOrEditProductDto createProductDto, Guid sellerId)
        {
            var product = await _productRepository.CreateProductAsync(sellerId, createProductDto);
            return StatusCode(201);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> PutProduct(Guid productId, [FromBody] CreateOrEditProductDto editProductDto)
        {
            var product = await _productRepository.EditProductAsync(productId, editProductDto);
            if (product == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            var product = await _productRepository.RemoveProductAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
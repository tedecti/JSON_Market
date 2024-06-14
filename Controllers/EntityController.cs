using JSON_Market.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JSON_Market.Controllers
{
    [Route("api")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly IEntityRepository _entityRepository;

        public EntityController(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        [HttpGet("seller")]
        public async Task<IActionResult> GetAllSellers()
        {
            var sellers = await _entityRepository.GetAllSellersAsync();
            return Ok(sellers);
        }

        [HttpGet("seller/{sellerId}")]
        public async Task<IActionResult> GetSellerById(Guid sellerId)
        {
            var seller = await _entityRepository.GetSellerByIdAsync(sellerId);
            return Ok(seller);
        }

        [HttpGet("seller/{sellerId}/products")]
        public async Task<IActionResult> GetCreatedProductsBySeller(Guid sellerId)
        {
            var products = await _entityRepository.GetCreatedProductsBySellerAsync(sellerId);
            return Ok(products);
        }

        [HttpGet("customer")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _entityRepository.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCustomerById(Guid customerId)
        {
            var customer = await _entityRepository.GetCustomerByIdAsync(customerId);
            return Ok(customer);
        }

        [HttpGet("customer/{customerId}/history")]
        public async Task<IActionResult> GetOrderHistoryByCustomer(Guid customerId)
        {
            var history = await _entityRepository.GetOrderHistoryByCustomerAsync(customerId);
            return Ok(history);
        }
    }
}
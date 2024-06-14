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
            if (!sellers.Any())
            {
                return NotFound();
            }

            return Ok(sellers);
        }

        [HttpGet("seller/{sellerId}")]
        public async Task<IActionResult> GetSellerById(Guid sellerId)
        {
            var seller = await _entityRepository.GetSellerByIdAsync(sellerId);
            if (seller == null)
            {
                return NotFound();
            }

            return Ok(seller);
        }

        [HttpGet("seller/{sellerId}/products")]
        public async Task<IActionResult> GetCreatedProductsBySeller(Guid sellerId)
        {
            var products = await _entityRepository.GetCreatedProductsBySellerAsync(sellerId);
            if (!products.Any())
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("customer")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _entityRepository.GetAllCustomersAsync();
            if (!customers.Any())
            {
                return NotFound();
            }

            return Ok(customers);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCustomerById(Guid customerId)
        {
            var customer = await _entityRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet("customer/{customerId}/history")]
        public async Task<IActionResult> GetOrderHistoryByCustomer(Guid customerId)
        {
            var history = await _entityRepository.GetOrderHistoryByCustomerAsync(customerId);
            if (!history.Any())
            {
                return NotFound();
            }
            return Ok(history);
        }
    }
}
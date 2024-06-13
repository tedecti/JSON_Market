using AutoMapper;
using JSON_Market.Models.Order;
using JSON_Market.Models.Order.GET;
using JSON_Market.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JSON_Market.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet("{userId}/customer")]
        public async Task<IActionResult> GetOrdersByCustomer(Guid customerId)
        {
            var orders = await _orderRepository.GetAllOrdersByCustomerAsync(customerId);
            var ordersMap = _mapper.Map<List<GetAllOrdersByCustomerDto>>(orders);
            return Ok(ordersMap);
        }
        
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            return Ok(order);
        }
        
        [HttpPost("{customerId}")]
        public async Task<IActionResult> PostOrder(Guid customerId, [FromBody] List<Guid> productIds)
        {
            var order = await _orderRepository.CreateOrderAsync(customerId, productIds);
            return Created();
        }
        
        [HttpPut("{customerId}")]
        public async Task<IActionResult> PutOrder([FromQuery]Guid orderId, [FromBody] List<Guid> productIds)
        {
            var order = await _orderRepository.EditOrderAsync(orderId, productIds);
            return NoContent();
        }
        
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            var order = await _orderRepository.RemoveOrderAsync(orderId);
            return NoContent();
        }
    }
}

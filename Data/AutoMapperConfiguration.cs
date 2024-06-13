using AutoMapper;
using JSON_Market.Models.Order;
using JSON_Market.Models.Order.GET;
using JSON_Market.Models.Product;
using JSON_Market.Models.Product.GET;

namespace JSON_Market.Data;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<Product, GetAllProductsDto>();
        CreateMap<Product, GetAllProductsBySellerDto>();
        CreateMap<Order, GetAllOrdersByCustomerDto>();
    }
}
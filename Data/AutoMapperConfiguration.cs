using AutoMapper;
using JSON_Market.Models;
using JSON_Market.Models.Customer;
using JSON_Market.Models.Customer.GET;
using JSON_Market.Models.Order;
using JSON_Market.Models.Order.GET;
using JSON_Market.Models.Product;
using JSON_Market.Models.Product.GET;
using JSON_Market.Models.Seller;
using JSON_Market.Models.Seller.GET;

namespace JSON_Market.Data;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<Product, GetAllProductsDto>();
        CreateMap<Product, GetAllProductsBySellerDto>();
        CreateMap<Product, ShortProductDto>();
        CreateMap<Order, GetAllOrdersByCustomerDto>();
        CreateMap<Order, GetAllOrdersDto>();
        CreateMap<Order, ShortOrderDto>();
        CreateMap<Seller, SellerDto>();
        CreateMap<Customer, CustomerDto>();
        CreateMap<OrderProduct, OrderProductDto>();
    }
}
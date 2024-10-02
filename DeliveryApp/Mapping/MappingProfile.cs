using AutoMapper;
using DeliveryApp.DTO.ProductDTO;
using DeliveryApp.DTO.UserDTO;
using DeliveryApp.Models;

namespace DeliveryApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap(); 
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderPart, OrderPartDto>().ReverseMap();
        }
    }
}

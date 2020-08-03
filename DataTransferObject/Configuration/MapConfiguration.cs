using AutoMapper;
using EcommerceWebApi.Domain.Models;
using EcommerceWebApi.Repository.Ecommerce;

namespace EcommerceWebApi.DataTransferObject.Configuration
{
    public class MapConfiguration : Profile
    {
        public MapConfiguration()
        {
            CreateMap<UserRequestDto, User>();

            CreateMap<CustomerRequestDto, Customer>();
            CreateMap<Customer, CustomerResponseDto>();

            CreateMap<ProductRequestDto, Product>();
            CreateMap<Product, ProductResponseDto>();

            CreateMap<Category, CategoryResponseDto>();

            CreateMap<CartProductInformation, CartProductResponseDto>();
        }
    }
}

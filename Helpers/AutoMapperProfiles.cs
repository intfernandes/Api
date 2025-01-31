
using Api.Dtos;
using Api.Entities;
using Api.Entities.Users;
using Api.Extensions;
using AutoMapper;

namespace Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {   
            CreateMap<Account, AccountDto>();

            CreateMap<Company, CompanyDto>()
                .ForMember(d => d.Members, o => o.MapFrom(s => s.Members.Count))
                .ForMember(d => d.Products, o => o.MapFrom(s => s.Products.Count))
                .ForMember(d => d.Orders, o => o.MapFrom(s => s.Orders.Count))
                .ForMember(d => d.AddressDto, o => o.MapFrom(s => s.Address!.Street + ", " + s.Address.City + ", " + s.Address.State + ", " + s.Address.ZipCode));

            CreateMap<Category, CategoryDto>();

            CreateMap<Address, AddressDto>();

            CreateMap<Customer, UserDto>();

            CreateMap<Member, UserDto>()
                .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.HasValue ? s.DateOfBirth.Value.CalculateAge() : (int?)null) )
                .ForMember(d => d.HighlightPhoto,
                    o => o.MapFrom(
                        s => s.Photos.FirstOrDefault(x => x.IsHighlight)!.Url
                    ));
                    
            CreateMap<Product, ProductDto>();

            CreateMap<Order, OrderDto>()
                .ForMember(d => d.TotalAmount, o => o.MapFrom(s => s.OrderItems.Sum(x => x.Price * x.Quantity)))
                .ForMember(d => d.OrderItems, o => o.MapFrom(s => s.OrderItems));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId , o => o.MapFrom(s => s.Product.Id ))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
                .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity));

            CreateMap<Photo, PhotoDto>();
            
        }
        
    }
}
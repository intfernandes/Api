


using Api.Dtos;
using Api.Entities;
using Api.Extensions;
using AutoMapper;

namespace Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {   
            CreateMap<Account, AccountDto>();

            CreateMap<Address, AddressDto>();

            CreateMap<Photo, PhotoDto>();

            
            CreateMap<Customer, CustomerDto>()
               .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.HasValue ? s.DateOfBirth.Value.CalculateAge() : (int?)null) );

            CreateMap<Employee, EmployeeDto>()
                .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.HasValue ? s.DateOfBirth.Value.CalculateAge() : (int?)null));


            CreateMap<Product, ProductDto>()
                .ForMember(d => d.Categories, o => o.MapFrom(s => s.Categories.Select(x => x.Id)))
                .ForMember(d => d.Photos, o => o.MapFrom(s => s.Photos));
            ;

            CreateMap<Order, OrderDto>()
                .ForMember(d => d.TotalAmount, o => o.MapFrom(s => s.OrderItems.Sum(x => x.Price * x.Quantity)))
                .ForMember(d => d.OrderItems, o => o.MapFrom(s => s.OrderItems));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId , o => o.MapFrom(s => s.Product.Id ))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
                .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity));

            CreateMap<Category, CategoryDto>();


           CreateMap<Store, StoreDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.AddressDto, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos));

        }
        
    }
}
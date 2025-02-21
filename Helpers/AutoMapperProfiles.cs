


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

            CreateMap<Category, CategoryDto>();
            
            CreateMap<Domain, DomainDto>()
                .ForMember(d => d.Members, o => o.MapFrom(s => s.Members.Count))
                .ForMember(d => d.Products, o => o.MapFrom(s => s.Products.Count))
                .ForMember(d => d.Orders, o => o.MapFrom(s => s.Orders.Count))
                .ForMember(d => d.AddressDto, o => o.MapFrom(s => s.Address!.Street + ", " + s.Address.City + ", " + s.Address.State + ", " + s.Address.ZipCode));

            CreateMap<Customer, CustomerDto>()
               .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.HasValue ? s.DateOfBirth.Value.CalculateAge() : (int?)null) )
               .ForMember(d => d.Address , o => o.MapFrom(s => s.Address))
               .ForMember(d => d.Orders, o => o.MapFrom(s => s.Orders))
               .ForMember(d => d.Photos, o => o.MapFrom(s => s.Photos))
               .ForMember(d => d.PaymentMethods, o => o.MapFrom(s => s.PaymentMethods))
               .ForMember(d => d.Reviews, o => o.MapFrom(s => s.Reviews))
               .ForMember(d => d.Ratings, o => o.MapFrom(s => s.Ratings))
               .ForMember(d => d.Carts, o => o.MapFrom(s => s.Carts))
               .ForMember(d => d.Wishlists, o => o.MapFrom(s => s.Wishlists))
               .ForMember(d => d.Subscriptions, o => o.MapFrom(s => s.Subscriptions))
               .ForMember(d => d.Feedbacks, o => o.MapFrom(s => s.Feedbacks))
               .ForMember(d => d.Complaints, o => o.MapFrom(s => s.Complaints))
               .ForMember(d => d.Refunds, o => o.MapFrom(s => s.Refunds))
               .ForMember(d => d.Reports, o => o.MapFrom(s => s.Reports))
               .ForMember(d => d.Chats, o => o.MapFrom(s => s.Chats))
               .ForMember(d => d.Conversations, o => o.MapFrom(s => s.Conversations))
               .ForMember(d => d.Messages, o => o.MapFrom(s => s.Messages))
               .ForMember(d => d.Notifications, o => o.MapFrom(s => s.Notifications))
               .ForMember(d => d.CurrentAccount, o => o.MapFrom(s => s.CurrentAccount))
               ;

            CreateMap<Member, MemberDto>()
                .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.HasValue ? s.DateOfBirth.Value.CalculateAge() : (int?)null) );

            CreateMap<Product, ProductDto>(); 

            CreateMap<Order, OrderDto>()
                .ForMember(d => d.TotalAmount, o => o.MapFrom(s => s.OrderItems.Sum(x => x.Price * x.Quantity)))
                .ForMember(d => d.OrderItems, o => o.MapFrom(s => s.OrderItems));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId , o => o.MapFrom(s => s.Product.Id ))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
                .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity));

            
        }
        
    }
}
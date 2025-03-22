
using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IOrdersRepository
    {
        Task<Order?> Create(OrderDto dto);
        Task<IEnumerable<Order>> Get();
        Task<IEnumerable<Order>?> Search(string input);
        Task<Order?> GetById(Guid id);
        Task<Order?> Update(OrderDto dto);
        Task<bool> Delete(Guid id);  
        Task<bool> Save();
        
    }
}
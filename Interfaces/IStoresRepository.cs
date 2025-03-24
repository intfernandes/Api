
using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IStoresRepository
    {
        Task<Store?> Create(StoreDto dto);
        Task<IEnumerable<Store>> Get();
        Task<IEnumerable<Store>?> Search(string input);
        Task<Store?> GetById(Guid id);
        Task<Store?> Update(StoreDto dto);
        Task<bool> Delete(Guid id);  
        Task<bool> Save();
        
    }
}
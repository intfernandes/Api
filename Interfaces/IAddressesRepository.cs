
using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address?> Create(AddressDto dto);
        Task<IEnumerable<Address>> Get();
        Task<IEnumerable<Address>?> Search(string input);
        Task<Address?> GetById(Guid id);
        Task<Address?> Update(AddressDto dto);
        Task<bool> Delete(Guid id);  
        Task<bool> Save();
        
    }
}


using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ICustomersRepository
    {
        Task<CustomerDto> Create(SignUpDto user);
        Task<IEnumerable<CustomerDto>> Get();
        Task<CustomerDto?> GetById(Guid id);
        Task<CustomerDto?> GetByName(string username);
        Task<CustomerDto?> GetByEmail(string email);
        Task<CustomerDto?> GetByPhoneNumber(string phoneNumber);
        Task<CustomerDto> Update(CustomerDto user);
        Task<bool> Delete(Guid id);  
        Task<bool> Save(); 
    }
}
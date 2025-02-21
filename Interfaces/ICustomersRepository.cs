

using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ICustomersRepository
    {
        Task<Customer?> Create(SignUpDto user);
        Task<Customer?> SignIn(SignInDto signIn);
        Task<IEnumerable<Customer>> Get();
        Task<Customer?> GetById(Guid id);
        Task<Customer?> GetByName(string username);
        Task<Customer?> GetByEmail(string email);
        Task<Customer?> GetByPhoneNumber(string phoneNumber);
        Task<Customer?> Update(CustomerDto user);
        Task<bool> Delete(Guid id);  
        Task<bool> Save(); 
    }
}
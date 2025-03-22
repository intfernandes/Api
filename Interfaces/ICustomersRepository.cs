

using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ICustomersRepository
    {
        Task<Customer?> Create(SignUpDto dto);
        Task<Customer?> SignIn(SignInDto dto);
        Task<IEnumerable<Customer>> Get();
        Task<IEnumerable<Customer>?> Search(string input);
        Task<Customer?> GetById(Guid id);
        Task<Customer?> Update(CustomerDto dto);
        Task<bool> Delete(Guid id);  
        Task<bool> Save(); 
    }
}
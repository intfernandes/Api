
using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IAccountsRepository
    {
        Task<Account?> Create(AccountDto dto);
        Task<IEnumerable<Account>> Get();
        Task<IEnumerable<Account>?> Search(string input);
        Task<Account?> GetById(Guid id);
        Task<Account?> Update(AccountDto dto);
        Task<bool> Delete(Guid id);  
        Task<bool> Save();
    }
}
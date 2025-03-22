
using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IDomainsRepository
    {
        Task<Domain?> Create(DomainDto dto);
        Task<IEnumerable<Domain>> Get();
        Task<IEnumerable<Domain>?> Search(string input);
        Task<Domain?> GetById(Guid id);
        Task<Domain?> Update(DomainDto dto);
        Task<bool> Delete(Guid id);  
        Task<bool> Save();
        
    }
}
using Api.Data;
using Api.Dtos;
using Api.Entities;
using Api.Interfaces; 
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class DomainsRepository(DataContext context) : IDomainsRepository
    {
    

        public async Task<Domain?> Create(DomainDto dto)
        {
            var domain = new Domain
            {
                Name = dto.Name,
                Description = dto.Description ?? "",
                Email = dto.Email,
            };

            await context.Domains.AddAsync(domain);
            await Save();
            return domain;
        }

        public async Task<IEnumerable<Domain>> Get()
        {
            return await context.Domains.ToListAsync();
        }

        public async Task<IEnumerable<Domain>?> Search(string input)
        {
            return await context.Domains
                .Where(x => x.Name.Contains(input) || x.Description.Contains(input))
                .ToListAsync();
        }

        public async Task<Domain?> GetById(Guid id)
        {
            return await context.Domains.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Domain?> Update(DomainDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            
            var domain = await GetById(dto.Id);

            if (domain == null) return null;

            domain.Name = dto.Name;
            domain.Description = dto.Description ?? domain.Description;
            domain.Email = dto.Email ?? domain.Email;

            await Save();
            return domain;
        }

        public async Task<bool> Delete(Guid id)
        {
            var domain = await GetById(id);

            if (domain == null) return false;

            context.Domains.Remove(domain);
            await Save();
            return true;
        }

        public async Task<bool> Save()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
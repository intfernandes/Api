using Api.Data;
using Api.Dtos;
using Api.Entities;
using Api.Interfaces; 
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class StoresRepository(DataContext context) : IStoresRepository
    {
    

        public async Task<Store?> Create(StoreDto dto)
        {
            var store = new Store
            {
                Name = dto.Name,
                Description = dto.Description ?? "",
                Email = dto.Email,
            };

            await context.Stores.AddAsync(store);
            await Save();
            return store;
        }

        public async Task<IEnumerable<Store>> Get()
        {
            return await context.Stores.ToListAsync();
        }

        public async Task<IEnumerable<Store>?> Search(string input)
        {
            return await context.Stores
                .Where(x => x.Name.Contains(input) || x.Description.Contains(input))
                .ToListAsync();
        }

        public async Task<Store?> GetById(Guid id)
        {
            return await context.Stores.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Store?> Update(StoreDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            
            var store = await GetById(dto.Id);

            if (store == null) return null;

            store.Name = dto.Name;
            store.Description = dto.Description ?? store.Description;
            store.Email = dto.Email ?? store.Email;

            await Save();
            return store;
        }

        public async Task<bool> Delete(Guid id)
        {
            var store = await GetById(id);

            if (store == null) return false;

            context.Stores.Remove(store);
            await Save();
            return true;
        }

        public async Task<bool> Save()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
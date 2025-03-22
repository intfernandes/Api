
using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class AddressRepository(DataContext context) : IAddressRepository
    {
        public async Task<Address?> Create(AddressDto dto)
        {
            var address = new Address
            {
                Street = dto.Street ?? "" ,
                City = dto.City ?? "" ,
                State = dto.State ?? "" ,
                ZipCode = dto.ZipCode,
                Country = dto.Country ?? "" ,
            };

            await context.Addresses.AddAsync(address);
            await Save();
            return address;
        }

        public async Task<IEnumerable<Address>> Get()
        {
            return await context.Addresses.ToListAsync();
        }

        public async Task<IEnumerable<Address>?> Search(string input)
        {
            return await context.Addresses
                .Where(x => x.Street.Contains(input) || x.City.Contains(input) || x.State.Contains(input) || (x.ZipCode != null && x.ZipCode.Contains(input)) || x.Country.Contains(input))
                .ToListAsync();
        }

        public async Task<Address?> GetById(Guid id)
        {
            return await context.Addresses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Address?> Update(AddressDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            
            var address = await GetById(dto.Id);

            if (address == null) return null;

            address.Street = dto.Street ?? address.Street; ; 
            address.City = dto.City ?? address.City; ; 
            address.State = dto.State ?? address.State; ; 
            address.ZipCode = dto.ZipCode;
            address.Country = dto.Country ?? address.Country; ; 

            await Save();
            return address;
            }

        public async Task<bool> Delete(Guid id)
        {
            var address = await GetById(id);

            if (address == null) return false;

            context.Addresses.Remove(address);
            await Save();
            return true;
        }

        public async Task<bool> Save()
        {
            return await context.SaveChangesAsync() > 0;
        }
    
    
    }
}
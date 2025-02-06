
using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class CustomersRepository(DataContext ctx, IMapper mapper) : ICustomersRepository
    {

   

        public async Task<IEnumerable<UserDto>> GetCustomersAsync()
        {
               var users = await ctx.Customers
            .Include(x => x.Photos)
            .ToListAsync();
            
            return mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetCustomerByIdAsync(Guid id)
        {
            var users = await ctx.Customers
            .Include(x => x.Photos)
            .FirstOrDefaultAsync(x => x.Id == id);

            return mapper.Map<UserDto>(users);
        }

        public async  Task<UserDto?> GetCustomerByUsernameAsync(string username)
        {
                   var users = await ctx.Customers
            .Include(x => x.Photos)
            .FirstOrDefaultAsync(x => x.FirstName == username);

            return mapper.Map<UserDto>(users);
        }

        public async  Task<UserDto?> GetCustomerByEmailAsync(string email)
        {
                  var users = await ctx.Customers
            .Include(x => x.Photos)
            .FirstOrDefaultAsync(x => x.Email == email);

            return mapper.Map<UserDto>(users);
        }

        public async  Task<UserDto?> GetCustomerByPhoneNumberAsync(string phoneNumber)
        {
                  var users = await ctx.Customers
            .Include(x => x.Photos)
            .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

            return mapper.Map<UserDto>(users);
        }

        public void Create(SignInDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            
            if(user != null) {
                ctx.Customers.Add(mapper.Map<Customer>(user));
            }

            
        }

        public void Update(UserDto user)
        {
            if(user != null) {
                ctx.Customers.Update(mapper.Map<Customer>(user));
            }
        }

        public async  Task<bool> DeleteUserAsync(Guid id)
        {
              var user = await ctx.Customers.FindAsync(id);
            
            if(user != null) {
                ctx.Customers.Remove(user);
                return await ctx.SaveChangesAsync() > 0;
                }

                return true;

        }

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
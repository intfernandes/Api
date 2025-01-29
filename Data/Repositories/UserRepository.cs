
using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class UserRepository(DataContext ctx, IMapper mapper) : IUserRepository
    {
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await ctx.Users
            .Include(x => x.Photos)
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await ctx.Users.ToListAsync();

        }

           public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await ctx.Users.FindAsync(id);
            
            if(user != null) {
                ctx.Users.Remove(user);
                return await ctx.SaveChangesAsync() > 0;
                }

                return true;

             

        }

        public async Task<bool> SaveAllAsync()
        {
            return await ctx.SaveChangesAsync() > 0;
        }

        public void Update(User user)
        {
            ctx.Entry(user).State = EntityState.Modified;
        }

        public async Task<IEnumerable<UserDto>> GetUsersDtosAsync()
        {
            return await ctx.Users 
            .ProjectTo<UserDto>(mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
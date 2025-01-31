
using Api.Dtos;
using Api.Entities.Users;
using Api.Interfaces;
using AutoMapper; 

namespace Api.Data.Repositories
{
    public class MembersRepository(DataContext ctx, IMapper mapper) : IMembersRepository
    {
        public void Create(SignInDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

              if(user != null) {
                ctx.Members.Add(mapper.Map<Member>(user));
            }      
        }

        public Task<bool> DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        // public async Task<UserDto?> GetMemberByEmailAsync(string email)
        // {
        //     return await ctx.Members
        //     .ProjectTo<UserDto>(mapper.ConfigurationProvider)
        //     .FirstOrDefaultAsync(x => x.Email == email);
        // }

        // public async Task<UserDto?> GetMemberByIdAsync(int id)
        // {
        //     return await ctx.Members
        //     .ProjectTo<UserDto>(mapper.ConfigurationProvider)
        //     .FirstOrDefaultAsync(x => x.Id == id);
        // }

        public Task<UserDto?> GetMemberByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        // public async Task<UserDto?> GetMemberByPhoneNumberAsync(string phoneNumber)
        // {
        //     return await ctx.Members
        //     .ProjectTo<UserDto>(mapper.ConfigurationProvider)
        //     .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        // }

        // public async Task<UserDto?> GetMemberByUsernameAsync(string username)
        // {
        //     return await ctx.Members
        //     .ProjectTo<UserDto>(mapper.ConfigurationProvider)
        //     .FirstOrDefaultAsync(x => x.UserName == username);
        // }

        // public async Task<IEnumerable<UserDto>> GetMembersAsync()
        // {
        //     return await ctx.Members
        //     .ProjectTo<UserDto>(mapper.ConfigurationProvider)
        //     .ToListAsync();
        // }

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(UserDto user)
        {
            throw new NotImplementedException();
        }

        Task<UserDto?> IMembersRepository.GetMemberByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        Task<UserDto?> IMembersRepository.GetMemberByPhoneNumberAsync(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        Task<UserDto?> IMembersRepository.GetMemberByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<UserDto>> IMembersRepository.GetMembersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
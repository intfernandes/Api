
using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IUserRepository
    {
        void Update(User user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<User>> GetUsersAsync();
        Task<IEnumerable<UserDto>> GetUsersDtosAsync();
        Task<User?> GetUserByIdAsync(int id);  
        Task<bool> DeleteUserAsync(int id);  
    }
}
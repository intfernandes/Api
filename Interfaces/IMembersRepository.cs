
using Api.Dtos;

namespace Api.Interfaces
{
    public interface IMembersRepository
    {
        Task<IEnumerable<UserDto>> GetMembersAsync();
        Task<UserDto?> GetMemberByIdAsync(Guid id);
        Task<UserDto?> GetMemberByUsernameAsync(string username);
        Task<UserDto?> GetMemberByEmailAsync(string email);
        Task<UserDto?> GetMemberByPhoneNumberAsync(string phoneNumber);
        void Create(SignInDto user);
        void Update(UserDto user);
        Task<bool> DeleteUserAsync(Guid id);  
        Task<bool> SaveAllAsync();   
    }
}
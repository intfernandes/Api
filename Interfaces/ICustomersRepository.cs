

using Api.Dtos; 

namespace Api.Interfaces
{
    public interface ICustomersRepository
    {
        Task<IEnumerable<UserDto>> GetCustomersAsync();
        Task<UserDto?> GetCustomerByIdAsync(Guid id);
        Task<UserDto?> GetCustomerByUsernameAsync(string username);
        Task<UserDto?> GetCustomerByEmailAsync(string email);
        Task<UserDto?> GetCustomerByPhoneNumberAsync(string phoneNumber);
        void Create(SignInDto user);
        void Update(UserDto user);
        Task<bool> DeleteUserAsync(Guid id);  
        Task<bool> SaveAllAsync(); 
    }
}
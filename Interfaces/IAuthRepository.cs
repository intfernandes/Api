
using Api.Dtos;

namespace Api.Interfaces
{
    public interface IAuthRepository
    {
        public Task<AuthResponseDto?> SignUpAsync(SignUpDto signUpDto);
        public Task<AuthResponseDto?> RegisterAsync(SignUpDto signUpDto);
        public Task<AuthResponseDto?> SignInAsync(SignInDto signInDto);
        public Task<string> SignOutAsync(string token);
        public Task<UserDto?> GetUserAsync(Guid id);
        public Task<bool> UserAlreadyExistsAsync(string email);
        
    }
}
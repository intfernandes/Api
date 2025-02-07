
using Api.Entities;

namespace Api.Interfaces
{
    public interface IUserRepository
    {
        Task<IUser?> GetUserByUsernameAsync(string username);
        Task<IUser?> GetUserByEmailAsync(string email);
        Task<bool> CheckIfUsernameExistsAsync(string username);
        Task<bool> CheckIfEmailExistsAsync(string email);
        Task<IUser?> SignupAsync(IUser user, string password); // Signup/Register User
        Task<IUser?> SigninAsync(string usernameOrEmail, string password); // Signin/Login User
        Task SignoutAsync(); // Conceptual Signout (more about interface completeness - session invalidation is usually handled elsewhere)
    }
}
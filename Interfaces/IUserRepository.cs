
// using Api.Dtos;
// using Api.Entities;

// namespace Api.Interfaces
// {
//     public interface IUserRepository
//     {
//         Task<IUser?> GetUserByUsernameAsync(string username);
//         Task<IUser?> GetUserByEmailAsync(string email);
//         Task<bool> CheckIfUsernameExistsAsync(string username);
//         Task<bool> CheckIfEmailExistsAsync(string email);
//         Task<IUser?> SignUpAsync(SignUpDto signUp); // Signup/Register User
//         Task<IUser?> SignInAsync(SignInDto signIn); // Signin/Login User
//         Task SignoutAsync(); // Conceptual Signout (more about interface completeness - session invalidation is usually handled elsewhere)
//     }
// }
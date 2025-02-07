
using System.Security.Cryptography;
using System.Text;
using Api.Entities;
using Api.Entities.Users;
using Api.Interfaces;
using Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class UserRepository(DataContext _context, TokenService _tokenService) : IUserRepository
    {
   

        public async Task<IUser?> GetUserByUsernameAsync(string username)
        {
            return await _context.Set<IUser>() // Use DbSet<IUser> to query for all types in the hierarchy
                                 .FirstOrDefaultAsync(u => u.FirstName == username);
        }

        public async Task<IUser?> GetUserByEmailAsync(string email)
        {
            return await _context.Users // Use DbSet<IUser>
                                 .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> CheckIfUsernameExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.FirstName  == username);
        }

        public async Task<bool> CheckIfEmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }


        public async Task<IUser?> SignupAsync(IUser user, string password)
        {
            if (await CheckIfUsernameExistsAsync(user.FirstName !)) // Username is assumed to be Required in IUser
            {
                return null; // Or throw an exception: throw new InvalidOperationException("Username already exists.");
            }
            if (await CheckIfEmailExistsAsync(user.Email!)) // Email is assumed to be Required in IUser
            {
                return null; // Or throw an exception: throw new InvalidOperationException("Email already exists.");
            }

            // Hash the password before saving
            using var hmac = new HMACSHA512();


            // Assuming IUser has a PasswordHash property to store the hashed password. Adjust if needed.
            if (user is Customer customer) // Example for concrete types, adjust based on your hierarchy
            {
               user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password) );
               user.PasswordSalt = hmac.Key;
                _context.Customers.Add((Customer)user); // Add to the appropriate DbSet (if using concrete DbSets for derived types)
            }
            else if (user is Member member)
            {
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password) );
                user.PasswordSalt = hmac.Key;
                _context.Members.Add((Member)user);
            }
            else
            {
                 // If IUser is an abstract class and you can create direct IUser instances (less common in TPH), handle it.
                 // Or, if you only have Customer/Member, and IUser is just an interface/abstract, this 'else' might not be needed or you could throw an exception:
                 // throw new ArgumentException("Invalid user type for signup.");
                 if(user is IUser baseUser) // If IUser is an abstract class
                 {
                 user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password) );
               user.PasswordSalt = hmac.Key;
                 _context.Users.Add((IUser)baseUser); // Add to DbSet<IUser> - for TPH this is likely what you'll use
                 } else {
                     throw new ArgumentException("Invalid user type for signup.");
                 }
            }


            await _context.SaveChangesAsync();
            return user; // Return the created user object (you might want to return a DTO instead of the entity in real apps)
        }


        public async Task<IUser?> SigninAsync(string usernameOrEmail, string password)
        {
            IUser? user = await GetUserByUsernameAsync(usernameOrEmail) ?? await GetUserByEmailAsync(usernameOrEmail);

            if (user == null)
            {
                return null; // User not found
            }

             using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return null;
        }

        user.Token =  _tokenService.CreateToken(user);
      

            return user; // Successful signin - return the user object (consider DTO for real apps)
        }


        public async Task SignoutAsync()
        {
            // Repository doesn't typically handle session invalidation directly.
            // Signout is usually managed at the application level (e.g., clearing authentication cookies/tokens, session state).
            // You might leave this method empty in the repository or potentially implement logic 
            // to clear any locally cached user data within the repository (if you were caching anything - which is generally not needed).
            // For now, we'll leave it empty to reflect that signout is usually a higher-level concern.
            await Task.CompletedTask; // Indicate async completion
        }
    }
}
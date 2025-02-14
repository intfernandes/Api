using System.Security.Cryptography;
using System.Text;
using Api.Dtos;
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
            return await _context
                .Set<IUser>() // Use DbSet<IUser> to query for all types in the hierarchy
                .FirstOrDefaultAsync(u => u.FirstName == username);
        }

        public async Task<IUser?> GetUserByEmailAsync(string email)
        {
            return await _context.Users // Use DbSet<IUser>
            .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> CheckIfUsernameExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.FirstName == username);
        }

        public async Task<bool> CheckIfEmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<IUser?> SignUpAsync(SignUpDto signUp)
        {
            if (await CheckIfEmailExistsAsync(signUp.Email!))
            {
                throw new InvalidOperationException("Email already exists.");
            }

            if (signUp.Password == null)
                throw new InvalidOperationException("Password is required.");

            // Hash the password before saving
            using var hmac = new HMACSHA512();

            if (signUp.CompanyId != null)
            {
                if (!Guid.TryParse(signUp.CompanyId.ToString(), out var companyId))
                {
                    throw new InvalidOperationException("Invalid CompanyId.");
                }

                var user = new Member
                {
                    FirstName = signUp.FirstName,
                    LastName = signUp.LastName,
                    Email = signUp.Email,
                    PhoneNumber = signUp.PhoneNumber,
                    DomainId = companyId,
                    DateOfBirth = signUp.DateOfBirth,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signUp.Password)),
                    PasswordSalt = hmac.Key,
                };

                _context.Members.Add((Member)user);
                user.Token = _tokenService.CreateToken(user);
                await _context.SaveChangesAsync();
                return user;
            }
            else
            {
                var user = new Customer
                {
                    FirstName = signUp.FirstName,
                    LastName = signUp.LastName,
                    Email = signUp.Email,
                    PhoneNumber = signUp.PhoneNumber,
                    DateOfBirth = signUp.DateOfBirth,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signUp.Password)),
                    PasswordSalt = hmac.Key,
                };
                _context.Customers.Add((Customer)user);
                user.Token = _tokenService.CreateToken(user);
                await _context.SaveChangesAsync();
                return user;
            }
        }

        public async Task<IUser?> SignInAsync(SignInDto signIn)
        {
            IUser? user =
                await GetUserByEmailAsync(signIn.Email);

            if (user == null)
            {
                return null;
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signIn.Password ));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return null;
            }

            user.Token = _tokenService.CreateToken(user);

            return user;
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

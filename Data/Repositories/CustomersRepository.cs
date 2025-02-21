
using System.Security.Cryptography;
using System.Text;
using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using AutoMapper; 
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class CustomersRepository(DataContext context, ITokenService tokenService) : ICustomersRepository
    {
        #region Authenthicate

           public async Task<Customer?> Create(SignUpDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            
            var cs = await context.Customers
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x =>
                    x.Email.Equals(user.Email.ToLower())
                );
                if (cs != null) throw new Exception("User already exists");
                
                using var hmac = new HMACSHA512();

                cs = new Customer
                {
                    FirstName = user.FirstName.ToLower() ,
                    LastName = user.LastName?.ToLower() ,
                    Email = user.Email.ToLower() ,
                    PhoneNumber = user.PhoneNumber,
                    DateOfBirth = user.DateOfBirth,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
                    PasswordSalt = hmac.Key
                };

                cs.Token = tokenService.CreateToken(cs);
                cs.RefreshTokens = [ tokenService.CreateRefreshToken(cs) ];

                context.Customers.Add(cs);
                
                await Save();

                return cs;
        }

           public async Task<Customer?> SignIn(SignInDto signin) {
            
            var cs = await context.Customers
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => 
                    x.Email.Equals(signin.Email.ToLower())
                );

                if(cs != null) {
                    using var hmac = new HMACSHA512(cs.PasswordSalt);

                    var pwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signin.Password));

                    for (int i = 0; i < pwdHash.Length; i++)
                    {
                        if (pwdHash[i] != cs.PasswordHash[i])
                            throw new Exception("Invalid Password");
                    }

                    cs.Token = tokenService.CreateToken(cs);
                    cs.RefreshTokens = [ tokenService.CreateRefreshToken(cs) ];

                    return cs;
                }

                return null;
           }


        #endregion

        #region Read
        public async Task<IEnumerable<Customer>> Get()
        {
            var users = await context.Customers
            .Include(x => x.Photos)
            .Include(x => x.Accounts)
            .Include(x => x.Address)
            .Include(x => x.Orders)
            .Where(x => x.IsDeleted == false)
            .ToListAsync();
            
            return users ;
        }

        public async Task<Customer?> GetById(Guid id)
        {
            var users = await context.Customers
            .Include(x => x.Photos)
            .Include(x => x.Accounts)
            .Include(x => x.Address)
            .Include(x => x.Orders)
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => x.Id == id);

            return users;
        }

        public async  Task<Customer?> GetByName(string username)
        {
                   var users = await context.Customers
            .Include(x => x.Photos)
            .Include(x => x.Accounts)
            .Include(x => x.Address)
            .Include(x => x.Orders)
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => x.FirstName == username);

            return users;
        }

        public async  Task<Customer?> GetByEmail(string email)
        {
                  var users = await context.Customers
            .Include(x => x.Photos)
            .Include(x => x.Accounts)
            .Include(x => x.Address)
            .Include(x => x.Orders)
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => x.Email == email);

            return users;
        }

        public async  Task<Customer?> GetByPhoneNumber(string phoneNumber)
        {
                  var users = await context.Customers
            .Include(x => x.Photos)
            .Include(x => x.Accounts)
            .Include(x => x.Address)
            .Include(x => x.Orders)
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

            return users;
        }

        #endregion

                #region Update

        public async Task<Customer> Update(CustomerDto user)
        {
            var cs = await context.Customers
                .Include(x => x.Photos)
                .Include(x => x.Accounts)
                .Include(x => x.Address)
                .Include(x => x.Orders)
                .Where(x => x.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == user.Id) ?? throw new Exception("Customer not found");

                cs.FirstName = user.FirstName ?? cs.FirstName ;
                cs.LastName = user.LastName ?? cs.LastName ;
                cs.Email = user.Email ?? cs.Email ;
                cs.PhoneNumber = user.PhoneNumber ?? cs.PhoneNumber ;
                cs.DateOfBirth = user.DateOfBirth ?? cs.DateOfBirth ;
                if( user.PasswordUpdate != null) {
                    using var hmac = new HMACSHA512();
                    cs.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordUpdate));
                    cs.PasswordSalt = hmac.Key;
                } 
                var res = await Save();
                if(!res) throw new Exception("Failed to update user");
                return cs;
        }
        #endregion


        #region Delete
        public async  Task<bool> Delete(Guid id)
        {
            var user = await context.Customers
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("User not found");

            if(user != null) {
               
                foreach (var account in  user.Accounts)
                {
                    account.AccountStatus = AccountStatus.Deleted;
                }

                user.IsDeleted = true;
                
                return await Save();
            }
            return false;

        }
        #endregion

        public async Task<bool> Save()
        {
            var res = await context.SaveChangesAsync();

            return res > 0;

        }
    }
}
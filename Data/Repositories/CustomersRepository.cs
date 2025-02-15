
using System.Security.Cryptography;
using System.Text;
using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class CustomersRepository(DataContext context, IMapper mapper) : ICustomersRepository
    {
        #region Create

           public async Task<CustomerDto> Create(SignUpDto user)
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

                context.Customers.Add(cs);
                
                await Save();

                return mapper.Map<CustomerDto>(cs);

            
        }
        #endregion

        #region Read
        public async Task<IEnumerable<CustomerDto>> Get()
        {
               var users = await context.Customers
            .Include(x => x.Photos)
            .Where(x => x.IsDeleted == false)
            .ToListAsync();
            
            return mapper.Map<IEnumerable<CustomerDto>>(users);
        }

        public async Task<CustomerDto?> GetById(Guid id)
        {
            var users = await context.Customers
            .Include(x => x.Photos)
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => x.Id == id);

            return mapper.Map<CustomerDto>(users);
        }

        public async  Task<CustomerDto?> GetByName(string username)
        {
                   var users = await context.Customers
            .Include(x => x.Photos)
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => x.FirstName == username);

            return mapper.Map<CustomerDto>(users);
        }

        public async  Task<CustomerDto?> GetByEmail(string email)
        {
                  var users = await context.Customers
            .Include(x => x.Photos)
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => x.Email == email);

            return mapper.Map<CustomerDto>(users);
        }

        public async  Task<CustomerDto?> GetByPhoneNumber(string phoneNumber)
        {
                  var users = await context.Customers
            .Include(x => x.Photos)
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

            return mapper.Map<CustomerDto>(users);
        }

        #endregion

                #region Update

        public async Task<CustomerDto> Update(CustomerDto user)
        {
            var cs = await context.Customers
                .Include(x => x.Photos)
                .Where(x => x.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == user.Id) ?? throw new Exception("User not found");

                cs.FirstName = user.FirstName ?? cs.FirstName ;
                cs.LastName = user.LastName ?? cs.LastName ;
                cs.Email = user.Email ?? cs.Email ;
                cs.PhoneNumber = user.PhoneNumber ?? cs.PhoneNumber ;
                cs.DateOfBirth = user.DateOfBirth ?? cs.DateOfBirth ;
                if( user.Password != null) {
                    using var hmac = new HMACSHA512();
                    cs.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                    cs.PasswordSalt = hmac.Key;
                } 
                var res = await Save();
                if(!res) throw new Exception("Failed to update user");
                return mapper.Map<CustomerDto>(cs);
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
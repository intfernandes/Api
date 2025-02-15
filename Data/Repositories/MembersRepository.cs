
using System.Security.Cryptography;
using System.Text;
using Api.Dtos;
using Api.Entities;
using Api.Entities.Users;
using Api.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class MembersRepository(DataContext context, IMapper mapper) : IMembersRepository
    {   
        #region Create
        public async Task<MemberDto?> Create(SignUpDto signUp)
        {
            if (signUp == null) throw new ArgumentNullException(nameof(signUp));

             var mb = await context.Members
             .Where(x => x.IsDeleted == false)
             .FirstOrDefaultAsync(x => 
                    x.Email.Equals(signUp.Email.ToLower())
                );
                if (mb != null) throw new Exception("User already exists");
                
                using var hmac = new HMACSHA512();

                mb = new Member
                {
                    FirstName = signUp.FirstName,
                    LastName = signUp.LastName,
                    Email = signUp.Email,
                    PhoneNumber = signUp.PhoneNumber,
                    DateOfBirth = signUp.DateOfBirth,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signUp.Password)),
                    PasswordSalt = hmac.Key
                };

                context.Members.Add(mb);
                
                 await Save();

                return mapper.Map<MemberDto>(mb); 
        }
        #endregion

        #region Update 

            public async Task<IEnumerable<MemberDto>> Get()
        {
            var members = await context.Members
            .Where(x => x.IsDeleted == false)
            .ToListAsync();

            return mapper.Map<IEnumerable<MemberDto>>(members);
        }

        public async Task<MemberDto?> GetById(Guid id)
        {
            var mb = await context.Members
            .Include(x => x.Photos)
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("User not found");
            return mapper.Map<MemberDto>(mb);
        }

        public async Task<MemberDto?> GetByEmail(string email)
        {
            var mb = await context.Members
            .Include(x => x.Photos)
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => 
                    x.Email.Equals(email.ToLower())
                ) ?? throw new Exception("User not found");

            return mapper.Map<MemberDto>(mb);
                
        }

        public async Task<MemberDto?> GetByPhoneNumber(string phoneNumber)
        {
            if (phoneNumber == null) throw new ArgumentNullException(nameof(phoneNumber));

            var mb = await context.Members
                .Include(x => x.Photos)
                .Where(x => x.IsDeleted == false)
                .FirstOrDefaultAsync(x => 
                    x.PhoneNumber != null && x.PhoneNumber.Equals(phoneNumber)
                ) ?? throw new Exception("User not found");

            return mapper.Map<MemberDto>(mb);
        }

        public async Task<MemberDto?> GetByName(string username)
        {
            var mb = await context.Members
            .Include(x => x.Photos)
            .FirstOrDefaultAsync(x =>
                x.IsDeleted == false &&
                username.Contains(x.FirstName)  || username.Contains(x.LastName ?? "")
                ) ?? throw new Exception("User not found");
                
            return mapper.Map<MemberDto>(mb);

        }

        #endregion

        #region Update
              public async Task<MemberDto?> Update(MemberDto user)
        {
                var mb = await context.Members
                    .Include(x => x.Accounts)
                    .FirstOrDefaultAsync(x => x.Id == user.Id) ?? throw new Exception("User not found");
              

                mb.FirstName = user.FirstName ?? mb.FirstName ;
                mb.LastName = user.LastName ?? mb.LastName ;
                mb.Email = user.Email ?? mb.Email ;
                mb.PhoneNumber = user.PhoneNumber ?? mb.PhoneNumber ;
                mb.DateOfBirth = user.DateOfBirth ?? mb.DateOfBirth ;
                if( user.Password != null) {
                    using var hmac = new HMACSHA512();
                    mb.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                    mb.PasswordSalt = hmac.Key;
                } 
                 var res = await Save();
                if(!res) throw new Exception("Failed to update user");
                return mapper.Map<MemberDto>(mb);
        }
        #endregion

        #region Delete
    
              public async Task<bool> Delete(Guid id)
        {
            var user = await context.Members
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
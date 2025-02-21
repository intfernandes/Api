
using System.Security.Cryptography;
using System.Text;
using Api.Dtos;
using Api.Entities;
using Api.Interfaces; 
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
       
    public class MembersRepository(DataContext context, ITokenService tokenService) : IMembersRepository
    {   
        #region Authenthicate
        public async Task<Member?> Create(SignUpDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            
            var mb = await context.Members
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x =>
                    x.Email.Equals(user.Email.ToLower())
                );
                if (mb != null) throw new Exception("Member already exists");
                
                using var hmac = new HMACSHA512();

                mb = new Member
                {
                    FirstName = user.FirstName.ToLower() ,
                    LastName = user.LastName?.ToLower() ,
                    Email = user.Email.ToLower() ,
                    PhoneNumber = user.PhoneNumber,
                    DateOfBirth = user.DateOfBirth,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
                    PasswordSalt = hmac.Key
                };

                mb.Token = tokenService.CreateToken(mb);
                mb.RefreshTokens = [ tokenService.CreateRefreshToken(mb) ];

                context.Members.Add(mb);
                
                await Save();

                return mb;
        }

        public async Task<Member?> SignIn(SignInDto signIn) {
            if(signIn == null || signIn.Email == null || signIn.Password == null) throw new ArgumentNullException(nameof(signIn));
            
            var mb = await context.Members
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => 
                    x.Email.Equals(signIn.Email.ToLower())
                );

                if(mb != null) {
                    using var hmac = new HMACSHA512(mb.PasswordSalt);

                    var pwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signIn.Password));

                    for (int i = 0; i < pwdHash.Length; i++)
                    {
                        if (pwdHash[i] != mb.PasswordHash[i]) throw new Exception("Invalid password");
                    }

                    mb.Token = tokenService.CreateToken(mb);
                    mb.RefreshTokens = [ tokenService.CreateRefreshToken(mb) ];

                    return mb;
                }
                
                return null;

        }

        #endregion

        #region Update 

            public async Task<IEnumerable<Member>> Get()
        {
            var members = await context.Members
            .Include(x => x.Photos)
            .Include(x => x.Accounts)
            .Include(x => x.Address)
            .Include(x => x.Orders)
            .Where(x => x.IsDeleted == false)
            .ToListAsync();

            return members;
        }

        public async Task<Member?> GetById(Guid id)
        {
            var mb = await context.Members
            .Include(x => x.Photos)
            .Include(x => x.Accounts)
            .Include(x => x.Address)
            .Include(x => x.Orders)
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("User not found");

            return mb;
        }

        public async Task<Member?> GetByEmail(string email)
        {
            var mb = await context.Members
            .Include(x => x.Photos)
            .Include(x => x.Accounts)
            .Include(x => x.Address)
            .Include(x => x.Orders)
            
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => 
                    x.Email.Equals(email.ToLower())
                ) ?? throw new Exception("User not found");

            return mb;
                
        }

        public async Task<Member?> GetByPhoneNumber(string phoneNumber)
        {
            if (phoneNumber == null) throw new ArgumentNullException(nameof(phoneNumber));

            var mb = await context.Members
                .Include(x => x.Photos)
                .Include(x => x.Accounts)
                .Include(x => x.Address)
                .Include(x => x.Orders)
                
                .Where(x => x.IsDeleted == false)
                .FirstOrDefaultAsync(x => 
                    x.PhoneNumber != null && x.PhoneNumber.Equals(phoneNumber)
                ) ?? throw new Exception("User not found");

            return mb;
        }

        public async Task<Member?> GetByName(string username)
        {
            var mb = await context.Members
            .Include(x => x.Photos)
            .Include(x => x.Accounts)
            .Include(x => x.Address)
            .Include(x => x.Orders)
            
            .FirstOrDefaultAsync(x =>
                x.IsDeleted == false &&
                username.Contains(x.FirstName)  || username.Contains(x.LastName ?? "")
                ) ?? throw new Exception("User not found");
                
            return mb;

        }

        #endregion

        #region Update
              public async Task<Member?> Update(MemberDto user)
        {
                var mb = await context.Members
                    .Include(x => x.Accounts)
                    .FirstOrDefaultAsync(x => x.Id == user.Id) ?? throw new Exception("User not found");
              

                mb.FirstName = user.FirstName ?? mb.FirstName ;
                mb.LastName = user.LastName ?? mb.LastName ;
                mb.Email = user.Email ?? mb.Email ;
                mb.PhoneNumber = user.PhoneNumber ?? mb.PhoneNumber ;
                mb.DateOfBirth = user.DateOfBirth ?? mb.DateOfBirth ;
                if( user.PasswordUpdate != null) {
                    using var hmac = new HMACSHA512();
                    mb.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordUpdate));
                    mb.PasswordSalt = hmac.Key;
                } 
                 var res = await Save();
                if(!res) throw new Exception("Failed to update user");
                return mb;
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
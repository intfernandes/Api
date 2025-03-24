
using System.Security.Cryptography;
using System.Text;
using Api.Dtos;
using Api.Entities;
using Api.Interfaces; 
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
       
    public class EmployeesRepository(DataContext context, ITokenService tokenService) : IEmployeesRepository
    {   
        #region Authenthicate
        public async Task<Employee?> Create(SignUpDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            
            var mb = await context.Employees
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x =>
                    x.Email.Equals(user.Email.ToLower())
                );
                if (mb != null) throw new Exception("Employee already exists");
                
                using var hmac = new HMACSHA512();

                mb = new Employee
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

                context.Employees.Add(mb);
                
                await Save();

                return mb;
        }

        public async Task<Employee?> SignIn(SignInDto signIn) {
            if(signIn == null || signIn.Email == null || signIn.Password == null) throw new ArgumentNullException(nameof(signIn));
            
            var mb = await context.Employees
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

            public async Task<IEnumerable<Employee>> Get()
        {
            var employees = await context.Employees
            .Include(x => x.Photos)
            .Include(x => x.Accounts)
            .Include(x => x.Address)
            .Include(x => x.Orders)
            .Where(x => x.IsDeleted == false)
            .ToListAsync();

            return employees;
        }

        public async Task<Employee?> GetById(Guid id)
        {
            var mb = await context.Employees
            .Include(x => x.Photos)
            .Include(x => x.Accounts)
            .Include(x => x.Address)
            .Include(x => x.Orders)
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("User not found");

            return mb;
        }

        public async Task<IEnumerable<Employee>?> Search(string input)
        {
          var employees = new List<Employee>();

            if(input == null) throw new ArgumentNullException(nameof(input));

            employees.AddRange(await GetByEmail(input));
            employees.AddRange(await GetByPhoneNumber(input));
            employees.AddRange(await GetByName(input));

            return employees;
        }

        public async Task<IEnumerable<Employee>> GetByEmail(string email)
        {
            var mb = await context.Employees
            .Include(x => x.Photos)
            .Include(x => x.Accounts)
            .Include(x => x.Address)
            .Include(x => x.Orders)
            .Where(x => x.IsDeleted == false && x.Email.Equals(email.ToLower()  ))
            .ToListAsync() ;

            return mb;
                
        }

        public async Task<IEnumerable<Employee>> GetByPhoneNumber(string phoneNumber)
        {
            if (phoneNumber == null) throw new ArgumentNullException(nameof(phoneNumber));

            var mb = await context.Employees
                .Include(x => x.Photos)
                .Include(x => x.Accounts)
                .Include(x => x.Address)
                .Include(x => x.Orders)
                .Where(x => x.IsDeleted == false && x.PhoneNumber != null && x.PhoneNumber.Equals(phoneNumber)  )
                .ToListAsync() ;

            return mb;
        }

        public async Task<IEnumerable<Employee>> GetByName(string username)
        {
            var mb = await context.Employees
            .Include(x => x.Photos)
            .Include(x => x.Accounts)
            .Include(x => x.Address)
            .Include(x => x.Orders)
            .Where(x => x.IsDeleted != false && username.Contains(x.FirstName)  || username.Contains(x.LastName ?? ""))
            .ToListAsync();
                
            return mb;

        }

        #endregion

        #region Update
              public async Task<Employee?> Update(EmployeeDto user)
        {
                var mb = await context.Employees
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
            var user = await context.Employees
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
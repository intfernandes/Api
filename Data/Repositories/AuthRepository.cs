
using System.ComponentModel.DataAnnotations;
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
    public class AuthRepository(DataContext ctx, ITokenService tokenService, IMapper mapper) : IAuthRepository
    {
        public async Task<UserDto?> GetUserAsync(Guid id)
        {
            var user = await ctx.Members.FindAsync(id);

            if (user == null) {
             var customer = await ctx.Customers.FindAsync(id);
            return mapper.Map<UserDto>(customer); 
            }
            
            return mapper.Map<UserDto>(user);
        }

        public Task<AuthResponseDto?> SignInAsync(SignInDto signInDto)
        {
            throw new NotImplementedException();
        }

        public async  Task<string> SignOutAsync(string token)
        {
            await ctx.SaveChangesAsync();

            return "Success";
        }

        public async Task<AuthResponseDto?> SignUpAsync(SignUpDto signUpDto)
        {
                   string? validate = await CheckEmail(signUpDto.Email,  signUpDto.AccountType);

            if(validate.Length > 0 ) return new AuthResponseDto { 
                Errors = [validate],
            };

            using var hmac = new HMACSHA512();


            if(signUpDto.AccountType == AccountType.Customer) {
                // add specific logics for customer 
                Customer cs = new() {
                FirstName = signUpDto.FirstName, 
                LastName = signUpDto.LastName,
                Email = signUpDto.Email.ToLower(),  
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signUpDto.Password) ),
                PasswordSalt = hmac.Key
                };

            ctx.Customers.Add(cs);
            await ctx.SaveChangesAsync();
            return new AuthResponseDto { 
                Token = tokenService.CreateToken(cs)
            };
            }



            var member = new Member {
                FirstName = signUpDto.FirstName, 
                LastName = signUpDto.LastName,
                Email = signUpDto.Email.ToLower(),  
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signUpDto.Password) ),
                PasswordSalt = hmac.Key
            };

            ctx.Members.Add(member);

            await ctx.SaveChangesAsync();

       
            return new AuthResponseDto { 
                Token = tokenService.CreateToken(member)
            };
        }

        public Task<bool> UserAlreadyExistsAsync(string email)
        {
            throw new NotImplementedException();
        }

                private async Task<string> CheckEmail(string email, AccountType userType ) {
             var attr = new EmailAddressAttribute();

             if(!attr.IsValid(email.ToLower())) return "E-mail is not an valid email";

            var alreadyExists =  false;

                    var users = await ctx.Members.FirstOrDefaultAsync(x => x.Email == email);

                    if(users != null) alreadyExists = true;

      

            //  if(userType == AccountType.Customer) {
            //      alreadyExists =  await ctx.Customers.AnyAsync(x => x.Email.ToLower()  == email.ToLower() );
         
            //  } else {
            //      alreadyExists =  await ctx.Members.AnyAsync(x => x.Email.ToLower()  == email.ToLower() );
           
            //  }
             

            if(alreadyExists) return "E-mail already in use";

            return "";
        }

    
    }
}
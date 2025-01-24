
using System.Security.Cryptography;
using System.Text;
using Api.Data;
using Api.Dtos;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Api.Interfaces;

namespace Api.Controllers
{
    public class AuthController(DataContext context, ITokenService tokenService) : BaseController
    {
        [HttpPost("register")] // auth/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto register) {

            string validate = await CheckEmail(register.Email);

            if(validate.Length > 0 ) return BadRequest(validate);

            using var hmac = new HMACSHA512();

            var user = new User {
                Name = register.Name,
                Email = register.Email.ToLower(), 
                Status = "inactive",
                PwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password) ),
                PwdSalt = hmac.Key
            };

            context.Users.Add(user);

            await context.SaveChangesAsync();
            
            return Ok(new UserDto {
                Name = user.Name,
                Email = user.Email,
                Gender = register.Gender,
                Status = "inactive",
                Token = tokenService.CreateToken(user)
            } );
        }

        [HttpPost("login")] // auth/login
        public async Task<ActionResult<UserDto>> Login(LoginDto login) {

            var user = await context.Users.FirstOrDefaultAsync(
                x => x.Email.Equals(login.Email.ToLower() ));

            if(user == null) return Unauthorized("User no found");

            using var hmac = new HMACSHA512(user.PwdSalt);
        
            var pwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            for (int i = 0; i < pwdHash.Length ; i++)
            {
                if(pwdHash[i] != user.PwdHash[i]) return Unauthorized("Invalid Password");
            }

            return Ok(new UserDto {
                Name = user.Name,
                Gender = user.Gender,
                Email = user.Email,
                Status = user.Status,
                Token = tokenService.CreateToken(user)
            });
        
        }

        private async Task<string> CheckEmail(string email) {
             var attr = new EmailAddressAttribute();

             if(!attr.IsValid(email.ToLower())) return "E-mail is not an valid email";
             
            var alreadyExists =  await context.Users.AnyAsync(x => x.Email.ToLower()  == email.ToLower() );

            if(alreadyExists) return "E-mail already in use";

            return "";
        }
    }
}
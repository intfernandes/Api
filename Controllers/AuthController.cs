
using System.Security.Cryptography;
using System.Text;
using Api.Data;
using Api.Dtos;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Api.Interfaces;
using AutoMapper;

namespace Api.Controllers
{
    public class AuthController(DataContext context, ITokenService tokenService) : BaseController
    {
        [HttpPost("register")] // auth/register
        public async Task<ActionResult<RegisterResponseDto>> Register(RegisterRequestDto register) {
            await Task.Delay(50);
     
            string validate = await CheckEmail(register.Email);

            if(validate.Length > 0 ) return BadRequest(validate);

            using var hmac = new HMACSHA512();

            var user = new User {
                Name = register.Name, 

                Email = register.Email.ToLower(), 
                Status = "active",
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password) ),
                PasswordSalt = hmac.Key
            };

            context.Users.Add(user);

            await context.SaveChangesAsync();

       
            return Ok(new RegisterResponseDto {
                Name = user.Name,
                Email = user.Email, 
                Token = tokenService.CreateToken(user)
            } );
        }

        [HttpPost("login")] // auth/login
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto login) {
 
            var user = await context.Users.FirstOrDefaultAsync(
                x => x.Email.Equals(login.Email.ToLower() ));

            if(user == null) return Unauthorized("User no found");

            using var hmac = new HMACSHA512(user.PasswordSalt);
        
            var pwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            for (int i = 0; i < pwdHash.Length ; i++)
            {
                if(pwdHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return Ok(new LoginResponseDto { 
                Email = user.Email, 
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
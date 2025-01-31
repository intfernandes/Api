
using System.Security.Cryptography;
using System.Text;
using Api.Data;
using Api.Dtos;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using Api.Interfaces;
using Api.Entities.Users;

namespace Api.Controllers
{
    public class AuthController(DataContext context, ITokenService tokenService, IAuthRepository auth ) : BaseController
    {
        [HttpPost("signup")] // auth/register
        public async Task<ActionResult<AuthResponseDto>> SignUp(SignUpDto signUp) {
            var result = await auth.SignUpAsync(signUp);
            if (result == null) return BadRequest("Sign up failed");
            return result;
       
     
        }

        [HttpPost("login")] // auth/login
        public async Task<ActionResult<AuthResponseDto>> SignIn(SignInDto signin) {

            IUser? user;

              if(signin.AccountType == AccountType.Customer) {
               user = await context.Customers.FirstOrDefaultAsync(
                x => x.Email.Equals(signin.Email.ToLower() ));
              } else {
                user = await context.Members.FirstOrDefaultAsync(
                x => x.Email.Equals(signin.Email.ToLower() ));
              } 


                if(user == null) return Unauthorized("User not found"); 

            using var hmac = new HMACSHA512(user.PasswordSalt);
        
            var pwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signin.Password));

            for (int i = 0; i < pwdHash.Length ; i++)
            {
                if(pwdHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return Ok(new AuthResponseDto {  
                Token = tokenService.CreateToken(user)
            });
        
        }


    }
}
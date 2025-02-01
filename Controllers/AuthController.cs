
using System.Security.Cryptography;
using System.Text;
using Api.Data;
using Api.Dtos;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using Api.Interfaces; 

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

         

              if(signin.AccountType == AccountType.Customer) {
              var cs = await context.Customers.FirstOrDefaultAsync(
                x => x.Email.Equals(signin.Email.ToLower() ));

            if(cs == null) return Unauthorized("User not found"); 
               using var hmac = new HMACSHA512(cs.PasswordSalt);
        
            var pwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signin.Password));

            for (int i = 0; i < pwdHash.Length ; i++)
            {
                if(pwdHash[i] != cs.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return Ok(new AuthResponseDto {  
                Token = tokenService.CreateToken(cs)
            });

              } else {
                var mb = await context.Members.FirstOrDefaultAsync(
                x => x.Email.Equals(signin.Email.ToLower() ));
                     if(mb == null) return Unauthorized("User not found"); 
               using var hmac = new HMACSHA512(mb.PasswordSalt);
        
            var pwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signin.Password));

            for (int i = 0; i < pwdHash.Length ; i++)
            {
                if(pwdHash[i] != mb.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return Ok(new AuthResponseDto {  
                Token = tokenService.CreateToken(mb)
            });
              } 



         
        
        }


    }
}
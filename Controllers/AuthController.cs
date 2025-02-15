using System.Security.Cryptography;
using System.Text;
using Api.Data; 
using Api.Dtos;
using Api.Entities; 
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class AuthController(
        DataContext context,
        ITokenService tokenService,
         ICustomersRepository customers,
         IMembersRepository members
    ) : V1Controller
    {
        [HttpPost("signup")] // api/v1/auth/signup
        public async Task<ActionResult<AuthResponseDto>> SignUp(SignUpDto signUp)
        {
            if (signUp == null) return BadRequest("Invalid request");

            if (signUp.Type == AccountType.Customer)
            { 
                var cs = await customers.Create(signUp);

                return Ok(new AuthResponseDto {
                    Token = tokenService.CreateToken(cs)
                });
            }
            else
            {
                var mb = await members.Create(signUp) ?? throw new Exception("An error occurred");

                  return Ok(new AuthResponseDto {
                    Token = tokenService.CreateToken(mb)
                });
            }
        
         
        }

        [HttpPost("signin")] // api/v1/auth/signin
        public async Task<ActionResult<AuthResponseDto>> SignIn(SignInDto signin)
        {
            if(signin == null || signin.Email == null || signin.Password == null) return BadRequest("Invalid request");
            
            var cs = await context.Customers.FirstOrDefaultAsync(x =>
                    x.Email.Equals(signin.Email.ToLower())
                );

             if(cs != null) {
                using var hmac = new HMACSHA512(cs.PasswordSalt);

                var pwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signin.Password));

                for (int i = 0; i < pwdHash.Length; i++)
                {
                    if (pwdHash[i] != cs.PasswordHash[i])
                        return Unauthorized("Invalid Password");
                }

                return Ok(new AuthResponseDto { Token = tokenService.CreateToken(cs) });
             }
             
             var mb = await context.Members.FirstOrDefaultAsync(x =>
                    x.Email.Equals(signin.Email.ToLower())
                );

            if (mb != null)
            {
                using var hmac = new HMACSHA512(mb.PasswordSalt);

                var pwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signin.Password));

                for (int i = 0; i < pwdHash.Length; i++)
                {
                    if (pwdHash[i] != mb.PasswordHash[i])
                        return Unauthorized("Invalid Password");
                }

                return Ok(new AuthResponseDto { Token = tokenService.CreateToken(mb) });
                
            }
            
            return Unauthorized("User not found");

            
        } 
    }
}

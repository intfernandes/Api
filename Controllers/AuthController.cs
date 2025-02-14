using System.Security.Cryptography;
using System.Text;
using Api.Data;
using Api.Data.Repositories;
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
        UserRepository users
    ) : BaseController
    {
        [HttpPost("signup")] // auth/signup
        public async Task<ActionResult<AuthResponseDto>> SignUp(SignUpDto signUp)
        {
            var result = await users.SignupAsync(signUp);
            if (result == null)
                return BadRequest("Sign up failed");
            return result;
        }

        [HttpPost("signin")] // auth/signin
        public async Task<ActionResult<AuthResponseDto>> SignIn(SignInDto signin)
        {
            if (signin.AccountType == AccountType.Customer)
            {
                var cs = await context.Customers.FirstOrDefaultAsync(x =>
                    x.Email.Equals(signin.Email.ToLower())
                );

                if (cs == null)
                    return Unauthorized("User not found");
                using var hmac = new HMACSHA512(cs.PasswordSalt);

                var pwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signin.Password));

                for (int i = 0; i < pwdHash.Length; i++)
                {
                    if (pwdHash[i] != cs.PasswordHash[i])
                        return Unauthorized("Invalid Password");
                }

                return Ok(new AuthResponseDto { Token = tokenService.CreateToken(cs) });
            }
            else
            {
                var mb = await context.Members.FirstOrDefaultAsync(x =>
                    x.Email.Equals(signin.Email.ToLower())
                );
                if (mb == null)
                    return Unauthorized("User not found");
                using var hmac = new HMACSHA512(mb.PasswordSalt);

                var pwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signin.Password));

                for (int i = 0; i < pwdHash.Length; i++)
                {
                    if (pwdHash[i] != mb.PasswordHash[i])
                        return Unauthorized("Invalid Password");
                }

                return Ok(new AuthResponseDto { Token = tokenService.CreateToken(mb) });
            }
        }

        [HttpPost("register")] // auth/signup
        public async Task<ActionResult<AuthResponseDto>> Register(SignUpDto signUp)
        {
            var result = await auth.RegisterAsync(signUp);
            if (result == null)
                return BadRequest("Sign up failed");
            return result;
        }
    }
}

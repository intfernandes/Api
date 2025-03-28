
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Entities;
using Api.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services
{
    public class TokenService(IConfiguration config) : ITokenService
    {
        public string CreateToken(dynamic user)
        {
            var secret = config["JwtSecret"] ?? throw new Exception("Cannot get JwtSecret frorm appsettings");

            if(secret.Length < 64) throw new Exception("The secret must be longer"); 

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var handler = new JwtSecurityTokenHandler();

            var token = handler.CreateToken(tokenDescriptor);
            
            return handler.WriteToken(token); 
        }

        public string CreateRefreshToken(dynamic user)
        {
            var secret = config["JwtSecret"] ?? throw new Exception("Cannot get JwtSecret frorm appsettings");

            if(secret.Length < 64) throw new Exception("The secret must be longer"); 

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.FirstName)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var handler = new JwtSecurityTokenHandler();

            var token = handler.CreateToken(tokenDescriptor);
            
            return handler.WriteToken(token); 
        }
    }
}
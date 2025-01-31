
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext ctx) {
            // if(await ctx.Users.AnyAsync() ) return;

            // var seed = await File.ReadAllTextAsync("Data/Seeds/UsersSeed.json");

            // var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true };

            // var users =  JsonSerializer.Deserialize<List<User>> (seed, options);

            // if (users == null) return;

            // foreach (var user in users)
            // {
            //     using var hmac = new HMACSHA512();

            //     user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123456"));
            //     user.PasswordSalt = hmac.Key;

            //     ctx.Users.Add(user); 
            // }

            await ctx.SaveChangesAsync();
        }
    }
}

using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers

{
[Authorize]
public class UsersController(DataContext context) : BaseController
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers() {
        var users = await context.Users.ToListAsync();

        return Ok(users);
    }

    [HttpGet("{id:int}")] // GET: api/v1/users/{id}
    [Authorize]
    public async Task<ActionResult<User>> GetUser(int Id) {
        var user = await context.Users.FindAsync(Id);

        if(user == null) {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPut("{id:int}")] // PUT: api/v1/users/{id}
    [ProducesResponseType(typeof(User), 200)]
    public async Task<ActionResult<User>> UpdateUser(int Id, User user) {
        var existingUser = await context.Users.FindAsync(Id);

        if(existingUser == null) {
            return NotFound();
        }

        if(user.Name.Length > 0) existingUser.Name = user.Name;
        if(user.Email.Length > 0) existingUser.Email = user.Email;
        
        await context.SaveChangesAsync();

        return Ok(existingUser);
    }

    [HttpDelete("{id:int}")] // DELETE: api/v1/users/{id}
    public async Task<ActionResult<User>> DeleteUser(int Id) {
        var user = await context.Users.FindAsync(Id);

        if(user == null) {
            return NotFound();
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync();

        return Ok(user);
} 
} }
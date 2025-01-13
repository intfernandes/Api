
using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers

{
[ApiController]
[Route("api/v1/[controller]")] // GET: api/v1/users
public class UsersController(DataContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers() {
        var users = await context.Users.ToListAsync();

        return Ok(users);
    }

      [HttpGet("{id:int}")] // GET: api/v1/users/{id}
    public async Task<ActionResult<User>> GetUser(int Id) {
        var user = await context.Users.FindAsync(Id);

        if(user == null) {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost] // POST: api/v1/users
    public async Task<ActionResult<User>> CreateUser(User user) {
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return Ok(user);
    }

    [HttpPut("{id:int}")] // PUT: api/v1/users/{id}
    [ProducesResponseType(typeof(User), 200)]
    public async Task<ActionResult<User>> UpdateUser(int Id, User user) {
        var existingUser = await context.Users.FindAsync(Id);

        if(existingUser == null) {
            return NotFound();
        }

        existingUser.Name = user.Name;
        existingUser.Email = user.Email;

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
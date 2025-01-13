
using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers

{
[ApiController]
[Route("api/v1/[controller]")] // api/v1/users
public class UsersController(DataContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers() {
        var users = await context.Users.ToListAsync();

        return Ok(users);
    }

      [HttpGet("{id:int}")] // api/v1/users/{id}
    public async Task<ActionResult<User>> GetUser(int Id) {
        var user = await context.Users.FindAsync(Id);

        if(user == null) {
            return NotFound();
        }

        return Ok(user);
    }
} 
}
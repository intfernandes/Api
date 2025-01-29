
using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

namespace Api.Controllers

{ 
[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers() {
        var users = await userRepository.GetUsersDtosAsync(); 

        return Ok(users);
    }

    [HttpGet("{id:int}")] // GET: api/v1/users/{id}
    public async Task<ActionResult<UserDto>> GetUser(int Id) {
        var user = await userRepository.GetUserByIdAsync(Id);

        if(user == null) {
            return NotFound();
        }

        var userDto = mapper.Map<UserDto>(user);


        return Ok(userDto);
    }

    [HttpPut("{id:int}")] // PUT: api/v1/users/{id}
    [ProducesResponseType(typeof(UserDto), 200)]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, UserDto user) {
        await Task.Delay(500);
        return Ok();

        // var existingUser = await userRepository.Update();

        // if(existingUser == null) return NotFound();

        // if(user?.Name?.Length > 0) existingUser.Name = user.Name;
        // if(user?.Email?.Length > 0) existingUser.Email = user.Email;
        
        // await userRepository.SaveAllAsync();

        // return Ok(UserDto.FromUser( existingUser));
    }

    [HttpDelete("{id:int}")] // DELETE: api/v1/users/{id}
    public async Task<ActionResult> DeleteUserAsync(int Id) {
        var user = await userRepository.DeleteUserAsync(Id);

   
        return Ok(new Dictionary<string, string>()
             {{"status","success"}});
} 
} }
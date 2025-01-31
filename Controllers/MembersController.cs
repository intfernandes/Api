
using Api.Dtos;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

namespace Api.Controllers

{ 
[Authorize]
public class MembersController(IMembersRepository members, IMapper mapper) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetMembers() {
        var result = await members.GetMembersAsync(); 

        return Ok(result);
    }

    [HttpGet("{id:int}")] // GET: api/v1/members/{id}
    public async Task<ActionResult<UserDto>> GetUser(Guid Id) {
        var result = await members.GetMemberByIdAsync(Id);

        if(result == null) return NotFound();
        
        var memberDto = mapper.Map<UserDto>(result);

        return Ok(memberDto);
    }

    [HttpPut("{id:int}")] // PUT: api/v1/members/{id}
    [ProducesResponseType(typeof(UserDto), 200)]
    public async Task<ActionResult<UserDto>> UpdateMember(MemberDto member) {
        var existingUser = await members.GetMemberByIdAsync(member.Id);

        if(existingUser == null) return NotFound();

        if(member?.FirstName ?.Length > 0) existingUser.FirstName = member.FirstName;
        if(member?.LastName?.Length > 0) existingUser.LastName = member.LastName;
        if(member?.Email?.Length > 0) existingUser.Email = member.Email;
        if(member?.Phone?.Length > 0) existingUser.Phone = member.Phone;
        if(member?.DateOfBirth != null ) existingUser.DateOfBirth = member.DateOfBirth;
        if(member?.Gender != null) existingUser.Gender = member.Gender;
        if(member?.Address != null ) existingUser.Address = member.Address;

        await members.SaveAllAsync();

        var memberDto = mapper.Map<UserDto>(existingUser);


  return Ok(memberDto);
    }

    [HttpDelete("{id:int}")] // DELETE: api/v1/Members/{id}
    public async Task<ActionResult> DeleteUserAsync(Guid Id) {
        var user = await members.DeleteUserAsync(Id);

   
        return Ok(new Dictionary<string, string>()
             {{"status","success"}});
} 
} }
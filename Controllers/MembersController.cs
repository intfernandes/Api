
using Api.Dtos;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

namespace Api.Controllers

{ 
[Authorize]
public class MembersController(IMembersRepository members, IMapper mapper) : V1Controller
{
    [HttpGet] // GET: api/v1/members
    public async Task<ActionResult<IEnumerable<MemberDto>>> Get() {
        var result = await members.Get(); 

        return Ok(result);
    }

    [HttpGet("{Id:Guid}")] // GET: api/v1/members/{id}
    public async Task<ActionResult<MemberDto>> GetById(Guid Id) {
        var result = await members.GetById(Id);

        if(result == null) return NotFound();
        
        var memberDto = mapper.Map<MemberDto>(result);

        return Ok(memberDto);
    }

        [HttpGet("name/{input:alpha}")] // GET: api/v1/members/name/{input}
    public async Task<ActionResult<MemberDto>> GetByUsername(string input) {
        var result = await members.GetByName(input);

        if(result == null) return NotFound();
        
        var memberDto = mapper.Map<MemberDto>(result);

        return Ok(memberDto);
    }

    [HttpGet("email/{input}")] // GET: api/v1/members/email/{input}
    public async Task<ActionResult<MemberDto>> GetByEmail(string input) {
        var result = await members.GetByEmail(input);

        if(result == null) return NotFound();
        
        var memberDto = mapper.Map<MemberDto>(result);

        return Ok(memberDto);
    }

    [HttpGet("phone/{input}")] // GET: api/v1/members/phone/{input}
    public async Task<ActionResult<MemberDto>> GetByPhoneNumber(string input) {
        var result = await members.GetByPhoneNumber(input);

        if(result == null) return NotFound();
        
        var memberDto = mapper.Map<MemberDto>(result);

        return Ok(memberDto);
    }

    [HttpPut("{id:Guid}")] // PUT: api/v1/members/{id}
    [ProducesResponseType(typeof(MemberDto), 200)]
    public async Task<ActionResult<MemberDto>> Update(MemberDto member) {
        var existingUser = await members.GetById(member.Id);

        if(existingUser == null) return NotFound();

        if(member?.FirstName ?.Length > 0) existingUser.FirstName = member.FirstName;
        if(member?.LastName?.Length > 0) existingUser.LastName = member.LastName;
        if(member?.Email?.Length > 0) existingUser.Email = member.Email;
        if(member?.PhoneNumber?.Length > 0) existingUser.PhoneNumber = member.PhoneNumber;
        if(member?.DateOfBirth != null ) existingUser.DateOfBirth = member.DateOfBirth;
        if(member?.Gender != null) existingUser.Gender = member.Gender;
        if(member?.Address != null ) existingUser.Address = member.Address;

        await members.Save();

        var memberDto = mapper.Map<MemberDto>(existingUser);


  return Ok(memberDto);
    }


    [HttpDelete("{id:Guid}")] // DELETE: api/v1/Members/{id}
    public async Task<ActionResult> Delete(Guid Id) {
        var user = await members.Delete(Id);

   
        return Ok(new Dictionary<string, string>()
             {{"status","success"}});
} 
} }
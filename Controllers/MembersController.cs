
using Api.Dtos;
using Api.Entities;
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

        var membersDtos = mapper.Map<IEnumerable<MemberDto>>(result);

        return Ok(membersDtos);
    }

    [HttpGet("{Id:Guid}")] // GET: api/v1/members/{id}
    public async Task<ActionResult<MemberDto>> GetById(Guid Id) {
        var result = await members.GetById(Id);

        if(result == null) return NotFound();
        
        var memberDto = mapper.Map<MemberDto>(result);

        return Ok(memberDto);
    }

    [HttpGet("/search={input:alpha}")] // GET: api/v1/members/search={input}
    public async Task<ActionResult<MemberDto>> Search(string input) {
        var result = await members.Search(input);

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
        if(member?.Gender != null) existingUser.Gender = member.Gender.GetValueOrDefault();

             if(member?.Address != null ) {
                if (existingUser.Address == null) existingUser.Address = new Address();
                
                var address = new Address {
                    Street = member.Address?.Street ?? existingUser.Address.Street,
                    City = member.Address?.City ?? existingUser.Address.City,
                    State = member.Address?.State ?? existingUser.Address.State,
                    Country = member.Address?.Country ?? existingUser.Address.Country,
                    ZipCode = member.Address?.ZipCode ?? existingUser.Address.ZipCode,
                };
                
                existingUser.Address = address;
                }

        await members.Save();

        var memberDto = mapper.Map<MemberDto>(existingUser);
        
        return Ok(memberDto);
    }


    [HttpDelete("{id:Guid}")] // DELETE: api/v1/Members/{id}
    public async Task<ActionResult> Delete(Guid Id) {
        try
        {
            
        await members.Delete(Id);
        return Ok(new { message = "Member deleted successfully" });

        }
        catch (Exception ex)
        {
            
           return BadRequest(new { message = ex.Message });

        }


} 
} }
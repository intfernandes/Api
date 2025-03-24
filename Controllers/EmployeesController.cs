
using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

namespace Api.Controllers

{ 
[Authorize]
public class EmployeesController(IEmployeesRepository Employees, IMapper mapper) : V1Controller
{
    [HttpGet] // GET: api/v1/Employees
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get() {
        var result = await Employees.Get(); 

        var employeesDtos = mapper.Map<IEnumerable<EmployeeDto>>(result);

        return Ok(employeesDtos);
    }

    [HttpGet("{Id:Guid}")] // GET: api/v1/Employees/{id}
    public async Task<ActionResult<EmployeeDto>> GetById(Guid Id) {
        var result = await Employees.GetById(Id);

        if(result == null) return NotFound();
        
        var employeeDto = mapper.Map<EmployeeDto>(result);

        return Ok(employeeDto);
    }

    [HttpGet("/search={input:alpha}")] // GET: api/v1/Employees/search={input}
    public async Task<ActionResult<EmployeeDto>> Search(string input) {
        var result = await Employees.Search(input);

        if(result == null) return NotFound();
        
        var employeeDto = mapper.Map<EmployeeDto>(result);

        return Ok(employeeDto);
    }

  
    [HttpPut("{id:Guid}")] // PUT: api/v1/Employees/{id}
    [ProducesResponseType(typeof(EmployeeDto), 200)]
    public async Task<ActionResult<EmployeeDto>> Update(EmployeeDto Employee) {
        var existingUser = await Employees.GetById(Employee.Id);

        if(existingUser == null) return NotFound();

        if(Employee?.FirstName ?.Length > 0) existingUser.FirstName = Employee.FirstName;
        if(Employee?.LastName?.Length > 0) existingUser.LastName = Employee.LastName;
        if(Employee?.Email?.Length > 0) existingUser.Email = Employee.Email;
        if(Employee?.PhoneNumber?.Length > 0) existingUser.PhoneNumber = Employee.PhoneNumber;
        if(Employee?.DateOfBirth != null ) existingUser.DateOfBirth = Employee.DateOfBirth;
        if(Employee?.Gender != null) existingUser.Gender = Employee.Gender.GetValueOrDefault();

             if(Employee?.Address != null ) {
                if (existingUser.Address == null) existingUser.Address = new Address();
                
                var address = new Address {
                    Street = Employee.Address?.Street ?? existingUser.Address.Street,
                    City = Employee.Address?.City ?? existingUser.Address.City,
                    State = Employee.Address?.State ?? existingUser.Address.State,
                    Country = Employee.Address?.Country ?? existingUser.Address.Country,
                    ZipCode = Employee.Address?.ZipCode ?? existingUser.Address.ZipCode,
                };
                
                existingUser.Address = address;
                }

        await Employees.Save();

        var employeeDto = mapper.Map<EmployeeDto>(existingUser);
        
        return Ok(employeeDto);
    }


    [HttpDelete("{id:Guid}")] // DELETE: api/v1/Employees/{id}
    public async Task<ActionResult> Delete(Guid Id) {
        try
        {
            
        await Employees.Delete(Id);
        return Ok(new { message = "Employee deleted successfully" });

        }
        catch (Exception ex)
        {
            
           return BadRequest(new { message = ex.Message });

        }


} 
} }
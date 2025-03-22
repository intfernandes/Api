using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    public class CustomersController(ICustomersRepository customers, IMapper mapper) : V1Controller
    {
           [HttpGet] // api/v1/customers
            public async Task<ActionResult<IEnumerable<CustomerDto>>> Get() {
                var result = await customers.Get(); 
                var customersDtos = mapper.Map<IEnumerable<CustomerDto>>(result);

             return Ok(customersDtos);
            }
    
            [HttpGet("{Id:Guid}")] // GET: api/v1/customers/{id}
            public async Task<ActionResult<CustomerDto>> GetById(Guid Id) {
                var result = await customers.GetById(Id);

                if(result == null) return NotFound();
                
                var customerDto = mapper.Map<CustomerDto>(result);

                return Ok(customerDto);
            }

         

            [HttpGet("/search={input:alpha}")] // GET: api/v1/customers/search={input}
            public async Task<ActionResult<CustomerDto>> Search(string input) {
                var result = await customers.Search(input);

                if(result == null) return NotFound();
                
                var customerDto = mapper.Map<CustomerDto>(result);

                return Ok(customerDto);
            }

        
      
            [HttpPut("{id:int}")]
            public async Task<ActionResult<Customer>> Update(int Id, Customer customer) {
            var existingUser = await customers.GetById(customer.Id);

            if(existingUser == null) return NotFound();

            if(customer?.FirstName ?.Length > 0) existingUser.FirstName = customer.FirstName;
            if(customer?.LastName?.Length > 0) existingUser.LastName = customer.LastName;
            if(customer?.Email?.Length > 0) existingUser.Email = customer.Email;
            if(customer?.PhoneNumber?.Length > 0) existingUser.PhoneNumber = customer.PhoneNumber;
            if(customer?.DateOfBirth != null ) existingUser.DateOfBirth = customer.DateOfBirth;
            if(customer?.Gender != null) existingUser.Gender = customer.Gender;
            if(customer?.Address != null ) {
                var address = new Address {
                    Street = customer.Address.Street,
                    City = customer.Address.City,
                    State = customer.Address.State,
                    Country = customer.Address.Country,
                    ZipCode = customer.Address.ZipCode
                };
                
                existingUser.Address = address;
                }

            await customers.Save();

            var customerDto = mapper.Map<CustomerDto>(existingUser);


            return Ok(customerDto);
            }
    
            [HttpDelete("{id:int}")]
            public async Task<ActionResult<Customer>> Delete(Guid Id) {
                var existingUser = await customers.GetById(Id);

                if(existingUser == null) return NotFound();

                var deleted = await customers.Delete(Id);

                return Ok(new { message = "Customer deleted successfully" });
            }    
        
    }
}
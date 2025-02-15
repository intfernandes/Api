using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

        public class CustomersController(ICustomersRepository customers, IMapper mapper) : V1Controller
    {
           [HttpGet] // api/v1/customers
            public async Task<ActionResult<IEnumerable<CustomerDto>>> Get() {
                var result = await customers.Get(); 

             return Ok(result);
            }
    
            [HttpGet("{Id:Guid}")] // GET: api/v1/customers/{id}
            public async Task<ActionResult<CustomerDto>> GetById(Guid Id) {
                var result = await customers.GetById(Id);

                if(result == null) return NotFound();
                
                var customerDto = mapper.Map<CustomerDto>(result);

                return Ok(customerDto);
            }

            [HttpGet("name/{input:alpha}")] // GET: api/v1/customers/name/{input}
            public async Task<ActionResult<CustomerDto>> GetByName(string input) {
                var result = await customers.GetByName(input);

                if(result == null) return NotFound();
                
                var customerDto = mapper.Map<CustomerDto>(result);

                return Ok(customerDto);
            }

            [HttpGet("email/{input}")] // GET: api/v1/customers/email/{input}
            public async Task<ActionResult<CustomerDto>> GetByEmail(string input) {
                var result = await customers.GetByEmail(input);

                if(result == null) return NotFound();
                
                var customerDto = mapper.Map<CustomerDto>(result);

                return Ok(customerDto);
            }

            [HttpGet("phone/{input}")] // GET: api/v1/customers/phone/{input}
            public async Task<ActionResult<CustomerDto>> GetByPhoneNumber(string input) {
                var result = await customers.GetByPhoneNumber(input);

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
            if(customer?.Address != null ) existingUser.Address = customer.Address;

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
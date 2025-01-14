
using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")] // GET: api/v1/customers
        public class CustomersController(DataContext context) : ControllerBase
    {
           [HttpGet]
            public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers() {
                var customers = await context.Customers.ToListAsync();
    
                return Ok(customers);
            }
    
            [HttpGet("{id:int}")]
            public async Task<ActionResult<Customer>> GetCustomer(int Id) {
                var customer = await context.Customers.FindAsync(Id);
    
                if(customer == null) {
                    return NotFound();
                }
    
                return Ok(customer);
            }
    
            [HttpPost]
            public async Task<ActionResult<Customer>> CreateCustomer(Customer customer) {
                context.Customers.Add(customer);
                await context.SaveChangesAsync();
                return Ok(customer);
            }
    
            [HttpPut("{id:int}")]
            public async Task<ActionResult<Customer>> UpdateCustomer(int Id, Customer customer) {
                var existingCustomer = await context.Customers.FindAsync(Id);
    
                if(existingCustomer == null) {
                    return NotFound();
                }
    
                if(customer.Name.Length > 0) existingCustomer.Name = customer.Name;
                if(customer.Email.Length > 0) existingCustomer.Email = customer.Email;
                if(customer.Phone.Length > 0) existingCustomer.Phone = customer.Phone;
                
                await context.SaveChangesAsync();
    
                return Ok(existingCustomer);
            }
    
            [HttpDelete("{id:int}")]
            public async Task<ActionResult<Customer>> DeleteCustomer(int Id) {
                var customer = await context.Customers.FindAsync(Id);
    
                if(customer == null) {
                    return NotFound();
                }
    
                context.Customers.Remove(customer);
                await context.SaveChangesAsync();
    
                return Ok(customer);
            }    
        
    }
}
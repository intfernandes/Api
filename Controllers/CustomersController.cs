
using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{

        public class CustomersController(DataContext context) : BaseController
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
    
                if(existingCustomer == null) return NotFound();

                if(customer?.FirstName != null) existingCustomer.FirstName = customer.FirstName;
                if(customer?.LastName != null) existingCustomer.LastName = customer.LastName;
                if(customer?.Email != null) existingCustomer.Email = customer.Email;
                if(customer?.PhoneNumber != null) existingCustomer.PhoneNumber = customer.PhoneNumber;
                if(customer?.Address != null) existingCustomer.Address = customer.Address; 
                
            
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
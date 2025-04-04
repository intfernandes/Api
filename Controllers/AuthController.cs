using Api.Dtos;
using Api.Entities; 
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AuthController( 
        ICustomersRepository customers,
        IEmployeesRepository Employees,
        IMapper mapper
    ) : V1Controller
    {
        [HttpPost("signup")] // api/v1/auth/signup
        public async Task<ActionResult<IUser>> SignUp(SignUpDto signUp)
        {
            if(signUp == null || signUp.FirstName == null || signUp.Email == null || signUp.Password == null) return BadRequest("First name, email and password are required");


            if (signUp.AccountType != null && signUp.AccountType != AccountType.Customer)
            { 
                var mb = await Employees.Create(signUp) ?? throw new Exception("An error occurred while creating a Employee");
                var employeeDto = mapper.Map<EmployeeDto>(mb); 
                return Ok(employeeDto);

            }
            else
            {
                var cs = await customers.Create(signUp) ?? throw new Exception("An error occurred while creating a customer");
                var customerDto = mapper.Map<CustomerDto>(cs);
                return Ok(customerDto);
            }
        
         
        }

        [HttpPost("signin")] // api/v1/auth/signin
        public async Task<ActionResult<UserDto>> SignIn(SignInDto signin)
        {
            if(signin == null || signin.Email == null || signin.Password == null) return BadRequest("Check your password or email");
            
            var cs = await customers.SignIn(signin);

             if(cs != null) return Ok(mapper.Map<CustomerDto>(cs));
        
             var mb = await Employees.SignIn(signin);

            if (mb != null) return Ok(mapper.Map<EmployeeDto>(mb));
            
            return Unauthorized("User not found");

            
        } 
    }
}

using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

namespace Api.Controllers
{
    public class BugsController(DataContext context) : V1Controller
    {
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetUnauthorized(){
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<Employee> GetNotFound(){
            var employee = context.Employees.Find(-1);

            if(employee == null)   return NotFound();

            return employee;
        }


    [HttpGet("server-error")]
    public ActionResult<Employee> GetServerError()
    {
        var thing = context.Employees.Find(-1) ?? throw new Exception("A bad thing has happened");
        return thing;
    }
    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("This was not a good request");
    }
        
    }
}
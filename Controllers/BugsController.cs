using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

namespace Api.Controllers
{
    public class BugsController(DataContext context) : BaseController
    {
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetUnauthorized(){
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<User> GetNotFound(){
            var user = context.Users.Find(-1);

            if(user == null)   return NotFound();

            return user;
        }


    [HttpGet("server-error")]
    public ActionResult<User> GetServerError()
    {
        var thing = context.Users.Find(-1) ?? throw new Exception("A bad thing has happened");
        return thing;
    }
    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("This was not a good request");
    }
        
    }
}
using Api.Data;
using Api.Entities;
using Api.Entities.Users;
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
        public ActionResult<Member> GetNotFound(){
            var member = context.Members.Find(-1);

            if(member == null)   return NotFound();

            return member;
        }


    [HttpGet("server-error")]
    public ActionResult<Member> GetServerError()
    {
        var thing = context.Members.Find(-1) ?? throw new Exception("A bad thing has happened");
        return thing;
    }
    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("This was not a good request");
    }
        
    }
}

using Api.Dtos;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    public class DomainsController(IDomainsRepository domains, IMapper mapper) : V1Controller
    {
        [HttpGet] // GET: api/v1/domains
        public async Task<ActionResult<IEnumerable<DomainDto>>> Get() {
            var result = await domains.Get(); 

            var domainsDtos = mapper.Map<IEnumerable<DomainDto>>(result);

            return Ok(domainsDtos);
        }

        [HttpGet("{Id:Guid}")] // GET: api/v1/domains/{id}
        public async Task<ActionResult<DomainDto>> GetById(Guid Id) {
            var result = await domains.GetById(Id);

            if(result == null) return NotFound();
            
            var domainDto = mapper.Map<DomainDto>(result);

            return Ok(domainDto);
        }

        [HttpGet("/search={input:alpha}")] // GET: api/v1/domains/search={input}
        public async Task<ActionResult<DomainDto>> Search(string input) {
            var result = await domains.Search(input);

            if(result == null) return NotFound();
            
            var domainDto = mapper.Map<DomainDto>(result);

            return Ok(domainDto);
        }

        [HttpPut("{id:Guid}")] // PUT: api/v1/domains/{id}
        [ProducesResponseType(typeof(DomainDto), 200)]
        public async Task<ActionResult<DomainDto>> Update(DomainDto domain) {
            var existingUser = await domains.GetById(domain.Id);

            if(existingUser == null) return NotFound();
            
            if(domain?.Name ?.Length > 0) existingUser.Name = domain.Name;
            if(domain?.Description?.Length > 0) existingUser.Description = domain.Description;
       
            return Ok(existingUser);
        }

        [HttpPost] // POST: api/v1/domains
        [ProducesResponseType(typeof(DomainDto), 201)]
        public async Task<ActionResult<DomainDto>> Create(DomainDto domain) {
            var result = await domains.Create(domain);

            if(result == null) return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        
    }
}
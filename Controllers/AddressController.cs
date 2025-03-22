
using Api.Dtos;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    public class AddressController(IAddressRepository addresses, IMapper mapper ) : V1Controller
    {
        [HttpGet] // GET: api/v1/address
        public async Task<ActionResult<IEnumerable<AddressDto>>> Get() {
            var result = await addresses.Get(); 

            var addressDtos = mapper.Map<IEnumerable<AddressDto>>(result);

            return Ok(addressDtos);
        }

        [HttpGet("{Id:Guid}")] // GET: api/v1/address/{id}
        public async Task<ActionResult<AddressDto>> GetById(Guid Id) {
            var result = await addresses.GetById(Id);

            if(result == null) return NotFound();
            
            var addressDto = mapper.Map<AddressDto>(result);

            return Ok(addressDto);
        }

        [HttpGet("/search={input:alpha}")] // GET: api/v1/address/search={input}
        public async Task<ActionResult<AddressDto>> Search(string input) {
            var result = await addresses.Search(input);

            if(result == null) return NotFound();
            
            var addressDto = mapper.Map<AddressDto>(result);

            return Ok(addressDto);
        }

        [HttpPut("{id:Guid}")] // PUT: api/v1/address/{id}
        [ProducesResponseType(typeof(AddressDto), 200)]
        public async Task<ActionResult<AddressDto>> Update(AddressDto address) {
            var existingUser = await addresses.GetById(address.Id);

            if(existingUser == null) return NotFound();

            if(address?.Street ?.Length > 0) existingUser.Street = address.Street;
            if(address?.City?.Length > 0) existingUser.City = address.City;
            if(address?.State?.Length > 0) existingUser.State = address.State;
            if(address?.Country?.Length > 0) existingUser.Country = address.Country;
            if(address?.ZipCode?.Length > 0) existingUser.ZipCode = address.ZipCode;
       
            return Ok(existingUser);
        }

        [HttpPost] // POST: api/v1/address
        [ProducesResponseType(typeof(AddressDto), 201)]
        public async Task<ActionResult<AddressDto>> Create(AddressDto address) {
            var result = await addresses.Create(address);
            if(result == null) return BadRequest();
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        
    }
}
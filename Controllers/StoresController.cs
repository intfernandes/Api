
using Api.Dtos;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    public class StoresController(IStoresRepository Stores, IMapper mapper) : V1Controller
    {
        [HttpGet] // GET: api/v1/Stores
        public async Task<ActionResult<IEnumerable<StoreDto>>> Get() {
            var result = await Stores.Get(); 

            var storesDtos = mapper.Map<IEnumerable<StoreDto>>(result);

            return Ok(storesDtos);
        }

        [HttpGet("{Id:Guid}")] // GET: api/v1/Stores/{id}
        public async Task<ActionResult<StoreDto>> GetById(Guid Id) {
            var result = await Stores.GetById(Id);

            if(result == null) return NotFound();
            
            var storeDto = mapper.Map<StoreDto>(result);

            return Ok(storeDto);
        }

        [HttpGet("/search={input:alpha}")] // GET: api/v1/Stores/search={input}
        public async Task<ActionResult<StoreDto>> Search(string input) {
            var result = await Stores.Search(input);

            if(result == null) return NotFound();
            
            var storeDto = mapper.Map<StoreDto>(result);

            return Ok(storeDto);
        }

        [HttpPut("{id:Guid}")] // PUT: api/v1/Stores/{id}
        [ProducesResponseType(typeof(StoreDto), 200)]
        public async Task<ActionResult<StoreDto>> Update(StoreDto Store) {
            var existingUser = await Stores.GetById(Store.Id);

            if(existingUser == null) return NotFound();
            
            if(Store?.Name ?.Length > 0) existingUser.Name = Store.Name;
            if(Store?.Description?.Length > 0) existingUser.Description = Store.Description;
       
            return Ok(existingUser);
        }

        [HttpPost] // POST: api/v1/Stores
        [ProducesResponseType(typeof(StoreDto), 201)]
        public async Task<ActionResult<StoreDto>> Create(StoreDto Store) {
            var result = await Stores.Create(Store);

            if(result == null) return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Api.Interfaces;
using AutoMapper;
using Api.Dtos;

namespace Api.Controllers
{
    [Authorize]
    public class ProductsController(IProductsRepository products, IMapper mapper) : V1Controller
    {
        [HttpGet] // GET: api/v1/products
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get() {
            var result = await products.Get(); 

            var productsDtos = mapper.Map<IEnumerable<ProductDto>>(result);

            return Ok(productsDtos);
        }

        [HttpGet("{Id:Guid}")] // GET: api/v1/products/{id}
        public async Task<ActionResult<ProductDto>> GetById(Guid Id) {
            var result = await products.GetById(Id);

            if(result == null) return NotFound();
            
            var productDto = mapper.Map<ProductDto>(result);

            return Ok(productDto);
        }

        [HttpGet("/search={input:alpha}")] // GET: api/v1/products/search={input}
        public async Task<ActionResult<ProductDto>> Search(string input) {
            var result = await products.Search(input);

            if(result == null) return NotFound();
            
            var productDto = mapper.Map<ProductDto>(result);

            return Ok(productDto);
        }

        [HttpPut("{id:Guid}")] // PUT: api/v1/products/{id}
        [ProducesResponseType(typeof(ProductDto), 200)]
        public async Task<ActionResult<ProductDto>> Update(ProductDto product) {
            var existingUser = await products.GetById(product.Id);

            if(existingUser == null) return NotFound();

            if(product?.Name ?.Length > 0) existingUser.Name = product.Name;
            if(product?.Description?.Length > 0) existingUser.Description = product.Description;
            if(product?.Price != null) existingUser.Price = product.Price;
       

            return Ok(existingUser);
        }

        [HttpPost] // POST: api/v1/products
        [ProducesResponseType(typeof(ProductDto), 201)]
        public async Task<ActionResult<ProductDto>> Create(ProductDto product) {
            var result = await products.Create(product);

            if(result == null) return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpDelete("{id:Guid}")] // DELETE: api/v1/products/{id}
        public async Task<ActionResult> Delete(Guid id) {
            var result = await products.Delete(id);

            if(result) return NoContent();

            return NotFound();
        }
    }
    
}

using Api.Dtos;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    public class CategoryController(ICategoriesRepository categories, IMapper mapper  ) : V1Controller
    {

        [HttpGet] // GET: api/v1/categories
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get() {
            var result = await categories.Get(); 

            var categoriesDtos = mapper.Map<IEnumerable<CategoryDto>>(result);

            return Ok(categoriesDtos);
        }

        [HttpGet("{Id:Guid}")] // GET: api/v1/categories/{id}
        public async Task<ActionResult<CategoryDto>> GetById(Guid Id) {
            var result = await categories.GetById(Id);

            if(result == null) return NotFound();
            
            var categoryDto = mapper.Map<CategoryDto>(result);

            return Ok(categoryDto);
        }

        [HttpGet("/search={input:alpha}")] // GET: api/v1/categories/search={input}
        public async Task<ActionResult<CategoryDto>> Search(string input) {
            var result = await categories.Search(input);

            if(result == null) return NotFound();
            
            var categoryDto = mapper.Map<CategoryDto>(result);

            return Ok(categoryDto);
        }

        [HttpPut("{id:Guid}")] // PUT: api/v1/categories/{id}
        [ProducesResponseType(typeof(CategoryDto), 200)]
        public async Task<ActionResult<CategoryDto>> Update(CategoryDto category) {
            var existingUser = await categories.GetById(category.Id);

            if(existingUser == null) return NotFound();
            
            if(category?.Name ?.Length > 0) existingUser.Name = category.Name;
            if(category?.Description?.Length > 0) existingUser.Description = category.Description;
       
            return Ok(existingUser);
        }
        
    }
}
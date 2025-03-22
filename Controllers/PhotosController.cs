
using Api.Dtos;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
[Authorize]
public class PhotosController( IPhotosRepository photos, IMapper mapper) : V1Controller 
    {
        [HttpGet] // GET: api/v1/photos
        public async Task<ActionResult<IEnumerable<PhotoDto>>> Get() {
            var result = await photos.Get(); 

            var photosDtos = mapper.Map<IEnumerable<PhotoDto>>(result);

            return Ok(photosDtos);
        }

        [HttpGet("{Id:Guid}")] // GET: api/v1/photos/{id}
        public async Task<ActionResult<PhotoDto>> GetById(Guid Id) {
            var result = await photos.GetById(Id);

            if(result == null) return NotFound();
            
            var photoDto = mapper.Map<PhotoDto>(result);

            return Ok(photoDto);
        }

        [HttpGet("/search={input:alpha}")] // GET: api/v1/photos/search={input}
        public async Task<ActionResult<PhotoDto>> Search(string input) {
            var result = await photos.Search(input);

            if(result == null) return NotFound();
            
            var photoDto = mapper.Map<PhotoDto>(result);

            return Ok(photoDto);
        }

        [HttpPut("{id:Guid}")] // PUT: api/v1/photos/{id}
        [ProducesResponseType(typeof(PhotoDto), 200)]
        public async Task<ActionResult<PhotoDto>> Update(PhotoDto dto) {
            var photo = await photos.GetById(dto.Id);

            if(photo == null) return NotFound();
            
            if(photo?.Description?.Length > 0) photo.Description = photo.Description;
       
            return Ok(photo);
        }
        
    }
}
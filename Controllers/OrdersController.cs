
using Api.Dtos;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    public class OrdersController( IOrdersRepository orders, IMapper mapper ) : V1Controller
    {
        [HttpGet] // GET: api/v1/orders
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get() {
            var result = await orders.Get(); 

            var ordersDtos = mapper.Map<IEnumerable<OrderDto>>(result);

            return Ok(ordersDtos);
        }

        [HttpGet("{Id:Guid}")] // GET: api/v1/orders/{id}
        public async Task<ActionResult<OrderDto>> GetById(Guid Id) {
            var result = await orders.GetById(Id);

            if(result == null) return NotFound();
            
            var orderDto = mapper.Map<OrderDto>(result);

            return Ok(orderDto);
        }

        [HttpGet("/search={input:alpha}")] // GET: api/v1/orders/search={input}
        public async Task<ActionResult<OrderDto>> Search(string input) {
            var result = await orders.Search(input);

            if(result == null) return NotFound();
            
            var orderDto = mapper.Map<OrderDto>(result);

            return Ok(orderDto);
        }

        [HttpPut("{id:Guid}")] // PUT: api/v1/orders/{id}
        [ProducesResponseType(typeof(OrderDto), 200)]
        public async Task<ActionResult<OrderDto>> Update(OrderDto dto) {
            var order = await orders.GetById(dto.Id);

            if(order == null) return NotFound();
       
            return Ok(order);
        }
        
    }
}
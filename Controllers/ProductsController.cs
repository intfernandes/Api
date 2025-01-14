using Microsoft.AspNetCore.Mvc;
using Api.Data;
using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")] // GET: api/v1/products
    public class ProductsController(DataContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() {
            var products = await context.Products.ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int Id) {
            var product = await context.Products.FindAsync(Id);

            if(product == null) {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product) {
            if(product.Name == null) return BadRequest("Name is required");
            if(product.Price == 0) return BadRequest("Price is required");
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int Id, Product product) {
            var existingProduct = await context.Products.FindAsync(Id);

            if(existingProduct == null) {
                return NotFound();
            }

            if(product.Name.Length > 0) existingProduct.Name = product.Name;
            if(product.Price > 0) existingProduct.Price = product.Price;
            
            await context.SaveChangesAsync();

            return Ok(existingProduct);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Product>> DeleteProduct(int Id) {
            var product = await context.Products.FindAsync(Id);

            if(product == null) {
                return NotFound();
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return Ok(product);
        }
    }
}
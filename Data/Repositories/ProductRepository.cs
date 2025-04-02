
using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class ProductRepository(DataContext context) : IProductsRepository
    {
        public async Task<Product?> Create(ProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description ?? "",
                Price = dto.Price,
            };

            await context.Products.AddAsync(product);
            await Save();
            return product;
        }

        public async Task<IEnumerable<Product>> Get()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>?> Search(string input)
        {
            return await context.Products
                .Where(x => x.Name.Contains(input) || (x.Description != null && x.Description.Contains(input)) || x.Price.ToString().Contains(input))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>?> Filter(string category)
        {
            var ids = category.Split('id=')[1].Split('&');
            var category = Guid.Parse(ids[0]);



            return await context.Products
                .Include(x => x.Categories)
                .Where(x => x.Categories.Any(x => x.Id == category))
                .ToListAsync();
        }

        public async Task<Product?> GetById(Guid id)
        {
            return await context.Products
            .Include(x => x.Categories)
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product?> Update(ProductDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            
            var product = await GetById(dto.Id);

            if (product == null) return null;

            product.Name = dto.Name;
            product.Description = dto.Description ?? product.Description;
            product.Price = dto.Price;

            await Save();
            return product;
        }

        public async Task<bool> Delete(Guid id)
        {
            var product = await GetById(id);

            if (product == null) return false;

            context.Products.Remove(product);
            await Save();
            return true;
        }

        public async Task<bool> Save()
        {
            return await context.SaveChangesAsync() > 0;
        
    }
}}
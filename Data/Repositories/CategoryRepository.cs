
using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class CategoryRepository(DataContext context) : ICategoriesRepository
    {
        public async Task<Category?> Create(CategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description ?? "",
            };

            await context.Categories.AddAsync(category);
            await Save();
            return category;
        }

        public async Task<IEnumerable<Category>> Get()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>?> Search(string input)
        {
            return await context.Categories
                .Where(x => x.Name.Contains(input) || (x.Description != null && x.Description.Contains(input)))
                .ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
            return await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category?> Update(CategoryDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            
            var category = await GetById(dto.Id);

            if (category == null) return null;

            category.Name = dto.Name;
            category.Description = dto.Description ?? category.Description;

            await Save();
            return category;
        }

        public async Task<bool> Delete(Guid id)
        {
            var category = await GetById(id);

            if (category == null) return false;

            context.Categories.Remove(category);
            await Save();
            return true;
        }

        public async Task<bool> Save()
        {
            return await context.SaveChangesAsync() > 0;
        }
        
    }
}
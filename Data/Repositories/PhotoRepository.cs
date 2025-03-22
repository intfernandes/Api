
using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class PhotoRepository(DataContext context) : IPhotosRepository
    {
        public async Task<Photo?> Create(PhotoDto dto)
        {
            var photo = new Photo
            {
                
                Description = dto.Description ?? "",
                CreatedAt = DateTime.Now,
                Highlight = dto.Highlight,
            };

            await context.Photos.AddAsync(photo);
            await Save();
            return photo;
        }

        public async Task<IEnumerable<Photo>> Get()
        {
            return await context.Photos.ToListAsync();
        }

        public async Task<IEnumerable<Photo>?> Search(string input)
        {
            return await context.Photos
                .Where(x => x.Description.Contains(input))
                .ToListAsync();
        }

        public async Task<Photo?> GetById(Guid id)
        {
            return await context.Photos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Photo?> Update(PhotoDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            
            var photo = await GetById(dto.Id);

            if (photo == null) return null;
            
            photo.Description = dto.Description ?? photo.Description;
            photo.Highlight = dto.Highlight;

            await Save();
            return photo;
        }

        public async Task<bool> Delete(Guid id)
        {
            var photo = await GetById(id);

            if (photo == null) return false;

            context.Photos.Remove(photo);
            await Save();
            return true;
        }

        public async Task<bool> Save()
        {
            return await context.SaveChangesAsync() > 0;
        }
        
    }
}

using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IPhotosRepository
    {
        Task<Photo?> Create(PhotoDto dto);
        Task<IEnumerable<Photo>> Get();
        Task<IEnumerable<Photo>?> Search(string input);
        Task<Photo?> GetById(Guid id);
        Task<Photo?> Update(PhotoDto dto);
        Task<bool> Delete(Guid id);  
        Task<bool> Save();
        
    }
}
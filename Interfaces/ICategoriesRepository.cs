using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ICategoriesRepository
    {
        Task<Category?> Create(CategoryDto dto);
        Task<IEnumerable<Category>> Get();
        Task<IEnumerable<Category>?> Search(string input);
        Task<Category?> GetById(Guid id);
        Task<Category?> Update(CategoryDto dto);
        Task<bool> Delete(Guid id);  
        Task<bool> Save(); 
    }
}
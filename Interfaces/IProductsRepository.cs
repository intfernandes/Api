

using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IProductsRepository
    {
        Task<Product?> Create(ProductDto dto);
        Task<IEnumerable<Product>> Get();
        Task<IEnumerable<Product>?> Search(string input);
        Task<Product?> GetById(Guid id);
        Task<Product?> Update(ProductDto dto);
        Task<bool> Delete(Guid id);  
        Task<bool> Save();
    }
}
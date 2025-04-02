
using Api.Entities;

namespace Api.Dtos
{
public class ProductDto : BaseEntity
{ 
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public string? Description { get; set; } = string.Empty;
    public List<Guid> Categories { get; set; } = [];
    public List<PhotoDto> Photos { get; set; } = [];
    public Guid? StoreId { get; set; }
}
}

using Api.Entities;

namespace Api.Dtos
{
public class ProductDto : BaseEntity
{ 
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public string? Description { get; set; } = string.Empty;
    public List<Guid> CategoryIds { get; set; } = [];
    public List<string> CategoryNames { get; set; } = []; 
    public List<PhotoDto> Photos { get; set; } = [];
}
}
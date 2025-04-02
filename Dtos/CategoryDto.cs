
using Api.Entities;

namespace Api.Dtos
{
public class CategoryDto : BaseEntity
{
    public Guid CategoryId { get; set; }
    public required string Name { get; set; } 
    public string Description { get; set; } = String.Empty;
    public List<PhotoDto> Photos { get; set; } = [];
}
}
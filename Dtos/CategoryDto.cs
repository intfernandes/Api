
namespace Api.Dtos
{
public class CategoryDto
{
    public Guid CategoryId { get; set; }
    public required string Name { get; set; } 
    public List<ProductDto> Products { get; set; } = [];
    public List<PhotoDto> Photos { get; set; } = [];

}
}
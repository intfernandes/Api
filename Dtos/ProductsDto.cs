
namespace Api.Dtos
{
public class ProductDto
{
    public int ProductId { get; set; } 
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public List<int> CategoryIds { get; set; } = [];
    public List<string> CategoryNames { get; set; } = []; 

}
}
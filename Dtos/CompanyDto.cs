
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
   public class DomainDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

    public virtual ICollection<UserDto>? Members { get; set; } = [];
    public virtual ICollection<ProductDto>? Products { get; set; } = [];
    public virtual ICollection<OrderDto>? Orders { get; set; } = [];
    public virtual AddressDto? AddressDto { get; set; }


}
}
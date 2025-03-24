
using System.ComponentModel.DataAnnotations;
using Api.Entities;

namespace Api.Dtos
{
   public class StoreDto : BaseEntity
{
    
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;
    [MaxLength(500)]
    public string? Description { get; set; }
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public required string Email { get; set; } = string.Empty;
    public virtual ICollection<UserDto>? Employees { get; set; } = [];
    public virtual ICollection<ProductDto>? Products { get; set; } = [];
    public virtual ICollection<OrderDto>? Orders { get; set; } = [];
    public virtual AddressDto? AddressDto { get; set; }
    public List<PhotoDto> Photos { get; set; } = [];


}
}
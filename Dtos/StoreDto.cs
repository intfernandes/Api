
using System.ComponentModel.DataAnnotations;
using Api.Entities;

namespace Api.Dtos
{
   public class StoreDto : BaseEntity
{
    
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(500)]
    public string? Description { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public required string Email { get; set; } = string.Empty;

    // TODO: transform those fields into new routes
    // public virtual ICollection<EmployeeDto>? Employees { get; set; } = [];
    // public virtual ICollection<ProductDto>? Products { get; set; } = [];
    // public virtual ICollection<OrderDto>? Orders { get; set; } = [];

    public virtual AddressDto? AddressDto { get; set; }
    public List<PhotoDto> Photos { get; set; } = [];


}
}
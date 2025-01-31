
using System.ComponentModel.DataAnnotations;

namespace Api.Entities
{
public class Product : AuditableBaseEntity
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;
    [MaxLength(500)]
    public string? Description { get; set; }
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
    public List<Photo> Photos { get; set; } = [];
    public int? HighlightPhotoId { get; set; }
    public virtual Photo? HighlightPhoto { get; set; }

    #region Relationships
    public Company Company { get; set; } = null!;
    public virtual Guid CompanyId { get; set; } 
    public virtual Guid OrderId { get; set; } 
    public virtual ICollection<Order> Order { get; set; } = [];
    public virtual ICollection<Category> Categories { get; set; } = [];

    #endregion

}
}



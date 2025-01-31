
using System.ComponentModel.DataAnnotations;

namespace Api.Entities
{
public class Category : AuditableBaseEntity
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

       #region Relationships
     public virtual ICollection<Product> Products { get; set; } = [];
    #endregion
}

public class ProductCategory
{
    public int ProductId { get; set; }
    public required Product Product { get; set; } 

    public int CategoryId { get; set; }
    public required Category Category { get; set; } 
}
}
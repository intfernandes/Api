
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

        public List<Photo> Photos { get; set; } = [];

        #region Relationships
        public virtual ICollection<Product> Products { get; set; } = [];
        #endregion
    }

    public class ProductCategory
    {
        #region Relationships

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        #endregion
    }
}
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

        #region Relationships
        public Guid? DomainId { get; set; }
        public virtual Domain? Domain { get; set; } = null!; // One-to-many: Product belongs to one Domain
        public virtual ICollection<Category> Categories { get; set; } = []; // Many-to-many: Product has many Categories

        #endregion
    }
}
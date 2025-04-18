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
        public Guid? StoreId { get; set; }
        public virtual Store? Store { get; set; } = null!;
        public virtual List<Category> Categories { get; set; } = [];
        #endregion
    }
}
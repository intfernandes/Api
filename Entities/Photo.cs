using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Entities.Users;

namespace Api.Entities
{
    [Table("Photos")]
    public class Photo : AuditableBaseEntity
    {
        [Required]
        [MaxLength(2048)]
        public string ImageUrl { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Highlight { get; set; }

        #region Relationships

        // Replaced CustomerId, MemberId, Customer, Member with generic IUser relationship:
        public Guid? UserId { get; set; }        // Foreign Key to IUser (replaces CustomerId & MemberId) - Nullable, exclusive with ProductId & CompanyId
        public virtual IUser? User { get; set; }  // Navigation to IUser (replaces Customer & Member)

        public Guid? ProductId { get; set; }     // Foreign Key to Product - Nullable, exclusive with UserId & CompanyId
        public virtual Product? Product { get; set; }

        // Add Company relationship:
        public Guid? CompanyId { get; set; }     // Foreign Key to Company - Nullable, exclusive with UserId & ProductId
        public virtual Company? Company { get; set; } // Navigation to Company

        #endregion
    }
}
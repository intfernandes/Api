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
        public Guid? UserId { get; set; }        // Foreign Key to IUser (replaces CustomerId & MemberId) - Nullable, exclusive with ProductId & DomainId
        public virtual IUser? User { get; set; }  // Navigation to IUser (replaces Customer & Member)

        public Guid? ProductId { get; set; }     // Foreign Key to Product - Nullable, exclusive with UserId & DomainId
        public virtual Product? Product { get; set; }

        // Add Domain relationship:
        public Guid? DomainId { get; set; }     // Foreign Key to Domain - Nullable, exclusive with UserId & ProductId
        public virtual Domain? Domain { get; set; } // Navigation to Domain

        #endregion
    }
}
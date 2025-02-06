using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
 public class AuditableBaseEntity : BaseEntity
    {
        public Guid? LastModifiedByEntityId { get; set; } // Tracks the ID of the Entity (Company, Member, Customer) that last updated this entity.
    }



    [Table("EntityAuditLogs")]
    public class EntityAuditLog : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string EntityType { get; set; } = null!;

        public Guid? EntityId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ActionType { get; set; } = null!;

        public string? Changes { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        public Guid? UserId { get; set; }
        // Now with Navigation Property to User:
        public virtual IUser? User { get; set; }  // Navigation Property to IUser who performed the action -  OPTIONAL relationship
    }

}


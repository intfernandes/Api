using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Api.Entities
{
    public class Account : AuditableBaseEntity
    {
        [MaxLength(20)]
        public List<string>? Permissions { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public AccountType AccountType { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public AccountStatus AccountStatus { get; set; }

        #region Relationships

        // Replaced MemberId, CustomerId, Member, Customer with generic IUser relationship:

        public Guid? UserId { get; set; }       
        public virtual IUser? User { get; set; }  
        public Guid? DomainId { get; set; }      
        public virtual Domain? Domain { get; set; }

        #endregion
    }
}
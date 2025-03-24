using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Api.Entities
{
    public class Account : AuditableBaseEntity
    {
        [MaxLength(20)]
        public List<string> Permissions { get; set; } = new List<string>();
        [Column(TypeName = "nvarchar(24)")]
        public AccountType AccountType { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public AccountStatus AccountStatus { get; set; }

        #region Relationships 

        public Guid? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public Guid? EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
        public Guid? StoreId { get; set; }      
        public virtual Store? Store { get; set; }

        #endregion
    }
}
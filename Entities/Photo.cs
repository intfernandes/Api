
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
    public string Url { get; set; } = null!;
    public bool IsHighlight { get; set; }

    #region Relationships

    public Guid? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } 

        public Guid? MemberId { get; set; }
    public virtual Member? Member { get; set; } 

    public Guid? ProductId { get; set; }
    public virtual Product? Product { get; set; }

    #endregion


}
}
using System.ComponentModel.DataAnnotations;
using Api.Entities.Users;

namespace Api.Entities 
{
public class Account : AuditableBaseEntity
{
    [Required]
    [MaxLength(255)]
    public string Instance { get; set; } = null!; 
    [MaxLength(20)]
    public List<string>? Permissions { get; set; } 
    public AccountType AccountType { get; set; } 
    public AccountStatus AccountStatus { get; set; } 

    #region Relationships
    public int? MemberId { get; set; }
    public virtual Member? Member { get; set; }
    public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } 
    public int? CompanyId { get; set; }
    public virtual Company? Company { get; set; } 

    #endregion
}
}


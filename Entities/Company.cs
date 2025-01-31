

using System.ComponentModel.DataAnnotations;
using Api.Entities.Users;

namespace Api.Entities
{
    public class Company : AuditableBaseEntity
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;
    [MaxLength(500)]
    public string? Description { get; set; }
    public virtual ICollection<Member> Members { get; set; } = [];
    public virtual ICollection<Product> Products { get; set; } = [];
    public virtual ICollection<Order> Orders { get; set; } = [];

    #region Relationships

    public int? AddressId { get; set; }
    public virtual Address? Address { get; set; }
    public int? AccountId { get; set; }
    public virtual Account? Account { get; set; }

    #endregion


}
}
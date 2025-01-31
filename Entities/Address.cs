

using System.ComponentModel.DataAnnotations;
using Api.Entities.Users;

namespace Api.Entities
{
    public class Address : BaseEntity
    {
    [Required]
    [MaxLength(255)]
    public string? Street { get; set; } 

    [Required]
    [MaxLength(100)]
    public string? City { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string? State { get; set; }

    
    [MaxLength(10)]
    public string? ZipCode { get; set; }

    public override bool Equals(object? obj) 
    {
        if (obj is Address otherAddress)
        {
            return Street == otherAddress.Street && City == otherAddress.City &&
                   State == otherAddress.State && ZipCode == otherAddress.ZipCode;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Street, City, State, ZipCode);
    }

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
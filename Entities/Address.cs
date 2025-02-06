using System.ComponentModel.DataAnnotations;
using Api.Entities.Users;

namespace Api.Entities
{
    public class Address : AuditableBaseEntity // Address is now Auditable
    {
        [Required]
        [MaxLength(255)]
        public string Street { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string City { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string State { get; set; } = null!;
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
        // No FK properties in Address itself
        #endregion
    }
}
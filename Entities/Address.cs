using System.ComponentModel.DataAnnotations;


namespace Api.Entities
{
    public class Address : AuditableBaseEntity // Address is now Auditable
    {
        [Required]
        [MaxLength(50)]
        public string Country { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string State { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string City { get; set; } = null!;
        [Required]
        [MaxLength(255)]
        public string  Street { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string? Square { get; set; } = null!;
        [MaxLength(50)]
        public string? Tower { get; set; } = null!;
        [Required]
        [MaxLength(255)]
        public string  Number { get; set; } = null!;
        [MaxLength(255)]
        public string? Floor { get; set; } = null!;
        [MaxLength(255)]
        public string? Apartment { get; set; } = null!;
        [MaxLength(255)]
        public string? Complement { get; set; } = null!;
        [MaxLength(10)]
        public string? ZipCode { get; set; }

        #region Relationships
        public Guid? StoreId { get; set; }
        public virtual Store? Store { get; set; }

        public Guid? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public Guid? EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }

        #endregion
    }
}
using System.ComponentModel.DataAnnotations;


namespace Api.Entities
{
    public class Domain : AuditableBaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public required string Email { get; set; } = string.Empty;
        [Required]
        public byte[] PasswordHash { get; set; } = [];
        [Required]
        public byte[] PasswordSalt { get; set; } = [];
        [MaxLength(500)]
        public string Description { get; set; } = String.Empty;

        #region Relationships

        [Required] // Enforcing Domain MUST have an Address
        public Guid AddressId { get; set; }
        public virtual Address? Address { get; set; } // One-to-one or one-to-zero-or-one: Domain has one Address

        [Required] // Enforcing Domain MUST have an Account
        public Guid AccountId { get; set; }
        public virtual Account? Account { get; set; } // One-to-one: Domain has one Account

        public virtual ICollection<Member> Members { get; set; } = []; // One-to-many: Domain has many Members
        public virtual ICollection<Product> Products { get; set; } = []; // One-to-many: Domain has many Products
        public virtual ICollection<Order> Orders { get; set; } = [];   // One-to-many: Domain receives many Orders
        public List<Photo> Photos { get; set; } = []; 

        #endregion
    }
}
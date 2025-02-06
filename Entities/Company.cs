using System.ComponentModel.DataAnnotations;
using Api.Entities.Users;

namespace Api.Entities
{
    public class Company : AuditableBaseEntity
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
        public string? Description { get; set; }

        #region Relationships

        [Required] // Enforcing Company MUST have an Address
        public Guid AddressId { get; set; }
        public virtual Address? Address { get; set; } // One-to-one or one-to-zero-or-one: Company has one Address

        [Required] // Enforcing Company MUST have an Account
        public Guid AccountId { get; set; }
        public virtual Account? Account { get; set; } // One-to-one: Company has one Account

        public virtual ICollection<Member> Members { get; set; } = []; // One-to-many: Company has many Members
        public virtual ICollection<Product> Products { get; set; } = []; // One-to-many: Company has many Products
        public virtual ICollection<Order> Orders { get; set; } = [];   // One-to-many: Company receives many Orders
        public List<Photo> Photos { get; set; } = [];


        #endregion
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    public abstract class IUser : AuditableBaseEntity 
    {

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public required string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(22)]
        public string? PhoneNumber { get; set; } = string.Empty;
        [Required]
        public byte[] PasswordHash { get; set; } = [];
        [Required]
        public byte[] PasswordSalt { get; set; } = [];
        public string Token { get; set;} = string.Empty;
        public string RefreshToken { get; set;} = string.Empty;
        public DateOnly? DateOfBirth { get; set; }
        public DateTime? LastActiveAt { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public Gender Gender { get; set; } = Gender.Unknown;
        public List<Photo> Photos { get; set; } = [];
        public Guid? HighlightPhotoId { get; set; }
        public virtual Photo? HighlightPhoto { get; set; }
        public Guid? AddressId { get; set; } 
        public virtual Address? Address { get; set; }
        public ICollection<Order> Orders { get; set; } = []; 
        public ICollection<Account> Accounts { get; set; } = []; // One-to-many: One IUser can have multiple Accounts (profile accounts)
        public Guid CurrentAccount { get; set; } 
    }
}
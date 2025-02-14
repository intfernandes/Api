using System.ComponentModel.DataAnnotations;
using Api.Entities;

public class SignUpDto
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
        public string Password { get; set; }  = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public Guid? CompanyId { get; set; } = null!;
        public string Token { get; set;} = string.Empty;
        public string RefreshToken { get; set;} = string.Empty;
        public DateOnly? DateOfBirth { get; set; } 
        public Gender Gender { get; set; } = Gender.Unknown;
        public List<Photo> Photos { get; set; } = [];
        public Guid? HighlightPhotoId { get; set; }
        public virtual Photo? HighlightPhoto { get; set; }
        public Guid? AddressId { get; set; } // Foreign Key for optional Address - still nullable for Users
        public virtual Address? Address { get; set; } // One-to-zero-or-one: IUser can have one Address (optional)
        public ICollection<Order> Orders { get; set; } = []; // Orders placed by this User (Customer or potentially Member as Customer)
        public ICollection<Account> Accounts { get; set; } = []; // One-to-many: One IUser can have multiple Accounts (profile accounts)
        public Guid CurrentAccount { get; set; } // Tracks the currently active Account for the IUser
} 
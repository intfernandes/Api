using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Api.Dtos;
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
        public Guid? DomainId { get; set; } = null!;
        public string? Token { get; set;} = string.Empty;
        public string? RefreshToken { get; set;} = string.Empty;
        public DateOnly? DateOfBirth { get; set; } 
        [JsonConverter(typeof(JsonStringEnumConverter<AccountType>))]
        public AccountType? AccountType { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter<Gender>))]
        public Gender Gender { get; set; } = Gender.Unknown;
        public List<PhotoDto> Photos { get; set; } = []; 
        public virtual AddressDto ? Address { get; set; } // One-to-zero-or-one: IUser can have one Address (optional)
        public ICollection<OrderDto > Orders { get; set; } = []; // Orders placed by this User (Customer or potentially Member as Customer)
        public ICollection<AccountDto > Accounts { get; set; } = []; // One-to-many: One IUser can have multiple Accounts (profile accounts)
        public Guid CurrentAccount { get; set; } // Tracks the currently active Account for the IUser
} 
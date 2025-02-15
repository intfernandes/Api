using System.ComponentModel.DataAnnotations;
using Api.Entities;

namespace Api.Dtos {
public class UserDto : BaseEntity
{
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string? Email { get; set; } = null!; 
        public string? PhoneNumber { get; set; } = null!; 
        public string? Password { get; set; } = null!; 
        public int? Age { get; set; }= null!; 
        public DateOnly? DateOfBirth { get; set; }= null!; 
        public DateTime? LastActiveAt { get; set; }= null!; 
        public Gender? Gender { get; set; } = null!;
        public List<Photo> Photos { get; set; } = [];
        public Guid? HighlightPhotoId { get; set; }= null!; 
        public virtual Photo? HighlightPhoto { get; set; }= null!; 
        public Guid? AddressId { get; set; } = null!; 
        public virtual Address? Address { get; set; }= null!; 
        public ICollection<Order> Orders { get; set; } = []; 
        public ICollection<Account> Accounts { get; set; } = []; // One-to-many: One IUser can have multiple Accounts (profile accounts)
        public Guid? CurrentAccount { get; set; } = null!;
        public bool? IsDomainResponsible { get; set; } = null!; 
}
}

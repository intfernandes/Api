using System.ComponentModel.DataAnnotations; 

namespace Api.Entities;

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
    public string? Phone { get; set; } = string.Empty; 
    [Required]
    public byte[] PasswordHash { get; set; } = [];
    [Required]
    public byte[] PasswordSalt { get; set; } = [];
    public DateOnly? DateOfBirth { get; set; }
    public DateTime? LastActiveAt { get; set; }
    public Gender? Gender { get; set; } 
    public Address? Address { get; set; } 
    public List<Photo> Photos { get; set; } = [];
    public Guid? HighlightPhotoId { get; set; }
    public Guid? AddressId { get; set; }
    public virtual Photo? HighlightPhoto { get; set; }
     public ICollection<Order> Orders { get; set; } = [];
    public  ICollection<Account> Accounts { get; set; } = [];


}
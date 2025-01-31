using System.ComponentModel.DataAnnotations;
using Api.Entities;

namespace Api.Dtos {
public class UserDto : BaseEntity
{
    [Required]
    [MaxLength(255)]
    public string? FirstName { get; set; } = string.Empty; 
    public string? LastName { get; set; } = string.Empty; 
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public  string? Email { get; set; } = string.Empty; 
    [Required] 
    [MaxLength(22)]
    public string? Phone { get; set; } = string.Empty; 
    public int? DateOfBirth { get; set; } 
    public int? Age { get; set; } 
    public Gender? Gender { get; set; } 
    public Address? Address { get; set; } 

     public ICollection<Order>? Orders { get; set; } = [];

     #region Relationships
    public virtual IEnumerable<Account>? Accounts { get; set; }
    public List<Photo>? Photos { get; set; } = [];
    public Guid? HighlightPhotoId { get; set; }
    public virtual Photo? HighlightPhoto { get; set; }
    
     #endregion

}
}

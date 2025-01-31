
using System.ComponentModel.DataAnnotations;
using Api.Entities;

namespace Api.Dtos
{
public class AccountDto 
{

    [Required]
    [MaxLength(255)]
    public string Instance { get; set; } = null!; 
    [MaxLength(20)]
    public List<string>? Permissions { get; set; } 
    public AccountType AccountType { get; set; } 
    public AccountStatus AccountStatus { get; set; } 
}
}
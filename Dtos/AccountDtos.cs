
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Api.Entities;

namespace Api.Dtos
{
public class AccountDto : BaseEntity
{
    [Required]
    [MaxLength(255)]
    public Guid? UserId { get; set; }  
    [MaxLength(20)]
    public Guid? DomainId { get; set; }
    public List<string> Permissions { get; set; } = new List<string>();
    [JsonConverter(typeof(JsonStringEnumConverter<AccountType>))]
    public AccountType? AccountType { get; set; } 
    [JsonConverter(typeof(JsonStringEnumConverter<AccountStatus>))]
    public AccountStatus? AccountStatus { get; set; } 
}
}
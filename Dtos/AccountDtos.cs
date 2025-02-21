
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
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
    [JsonConverter(typeof(JsonStringEnumConverter<AccountType>))]
    public AccountType AccountType { get; set; } 
    [JsonConverter(typeof(JsonStringEnumConverter<AccountStatus>))]
    public AccountStatus AccountStatus { get; set; } 
}
}
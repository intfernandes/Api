
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Api.Entities;

namespace Api.Dtos
{
public class AccountDto : BaseEntity
{
    [MaxLength(40)]
    public Guid? CustomerId { get; set; }  
    [MaxLength(40)]
    public Guid? EmployeeId { get; set; }  
    [MaxLength(40)]
    public Guid? StoreId { get; set; }
    public List<string> Permissions { get; set; } = new List<string>();
    [JsonConverter(typeof(JsonStringEnumConverter<AccountType>))]
    public AccountType? AccountType { get; set; } 
    [JsonConverter(typeof(JsonStringEnumConverter<AccountStatus>))]
    public AccountStatus? AccountStatus { get; set; } 
}
}
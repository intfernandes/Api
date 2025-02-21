
using System.Text.Json.Serialization;
using Api.Entities;

namespace Api.Dtos {
public class UserDto : BaseEntity
{
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string? Email { get; set; } = null!; 
        public string? PhoneNumber { get; set; } = null!; 
        public string? PasswordUpdate { get; set; } = null!; 
        public int? Age { get; set; }= null!; 
        public DateOnly? DateOfBirth { get; set; }= null!; 
        public DateTime? LastActiveAt { get; set; }= null!; 
        [JsonConverter(typeof(JsonStringEnumConverter<Gender>))]
        public Gender? Gender { get; set; } = null!;
        public List<PhotoDto> Photos { get; set; } = [];
        public Guid? AddressId { get; set; } = null!; 
        public virtual AddressDto? Address { get; set; }= null!; 
        public ICollection<OrderDto> Orders { get; set; } = []; 
        public ICollection<AccountDto> Accounts { get; set; } = [];
        public Guid? CurrentAccount { get; set; } = null!;
        public bool? IsDomainResponsible { get; set; } = null!; 
        public string Token { get; set;} = string.Empty;
        public ICollection<string> RefreshTokens { get; set; } = [];
}
}

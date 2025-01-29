using Api.Extensions;

namespace Api.Entities;

public class User
{
    public int Id { get; set;}
    public required string Name { get; set;} = string.Empty;
    public required string Email { get; set;}
    public  string Status { get; set;}= string.Empty; 
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public DateOnly DateOfBirth { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastActiveAt { get; set; } = DateTime.UtcNow;
    public string Gender { get; set; }= string.Empty;  
    public  string City { get; set; }= string.Empty; 
    public  string Country { get; set; }= string.Empty; 
    public List<Photo> Photos { get; set; } = [];


    // public int GetAge() {
    //     return DateOfBirth.CalculateAge();
    // }

}

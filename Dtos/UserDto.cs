
using Api.Entities;

namespace Api.Dtos
{
    public class UserDto
    {
    public int Id { get; set; }
    public string? Name { get; set; }
    public required string Email { get; set;}
    public  string Status { get; set;}= string.Empty; 
    public int Age { get; set; }
    public string? PhotoUrl { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime LastActiveAt { get; set; }
    public string? Gender { get; set; } 
    public string? City { get; set; }
    public string? Country { get; set; }
    public List<PhotoDto>? Photos { get; set; }
    }
}
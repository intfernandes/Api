
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class RegisterRequestDto
    {
    [MaxLength(length: 32)]
    public string Name { get; set;} = string.Empty;

    [Required]
    [MaxLength(length: 32)]
    public required string Email { get; set;}

    [Required]
    [MinLength(length: 6)]
    [MaxLength(length: 32)]
    public required string Password { get; set; } 

    }
}
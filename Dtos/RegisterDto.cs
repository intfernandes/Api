
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class RegisterDto
    {
    [Required]
    [MaxLength(length: 32)]

    public required string Name { get; set;}
    [Required]
    [MaxLength(length: 32)]

    public required string Email { get; set;}
    [Required]
    [MinLength(length: 6)]
    [MaxLength(length: 32)]
    public required string Password { get; set; }
    }
}
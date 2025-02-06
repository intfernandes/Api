
namespace Api.Dtos
{
    using System.ComponentModel.DataAnnotations;
    using Api.Entities;

    // DTOs for Authentication

    public class SignUpDto
{
    [Required]
    [MaxLength(255)]
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 6)] 
    public string Password { get; set; } = null!;

    [Compare("Password", ErrorMessage = "Passwords do not match")] 
    public string ConfirmPassword { get; set; } = null!; 

   
}

public class SignInDto
{
    [Required]
    [MaxLength(255)]
    public string Email { get; set; } = null!; 

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = null!;

    [Required]
    public AccountType AccountType { get; set; } 
}

public class SignOutDto
{ 
    public string? Token { get; set; } 
}


public class AuthResponseDto
{
    public string? Token { get; set; } = null!; 
    public string? RefreshToken { get; set; } 
    public IEnumerable<String>? Errors;
}
}
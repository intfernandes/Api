using System.ComponentModel.DataAnnotations;





public class CustomerDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;

    public AddressDto? Address { get; set; }

    [Phone] // More specific attribute
    [MaxLength(20)]
    public string? Phone { get; set; } // Consistent naming (Phone, not PhoneNumber)

    public int AccountId { get; set; }
}

public class AddressDto
{
    [MaxLength(255)]
    public string? Street { get; set; }

    [MaxLength(100)]
    public string? City { get; set; }

    [MaxLength(50)]
    public string? State { get; set; }

    [MaxLength(10)]
    [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip Code")] // Regex for zip code
    public string? ZipCode { get; set; }
}

public class UserRegistrationDto
{
    [Required]
    [MaxLength(255)]
    public string Username { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = null!;
}

public class UserLoginDto
{
    [Required]
    [MaxLength(255)]
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = null!;
}
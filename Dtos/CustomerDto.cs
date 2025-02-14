
using System.ComponentModel.DataAnnotations; 

namespace Api.Dtos
{
public class CustomerDto : UserDto
{
    [MaxLength(255)]
    public string? DomainName { get; set; }

    [MaxLength(255)]
    public string? ContactName { get; set; }

    [MaxLength(255)]
    public string? Website { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }

    public double Rating { get; set; }
}
}
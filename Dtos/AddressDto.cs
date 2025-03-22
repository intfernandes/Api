
using System.ComponentModel.DataAnnotations;
using Api.Entities;

namespace Api.Dtos
{
   public class AddressDto : BaseEntity
{
    [MaxLength(255)]
    public string? Street { get; set; }

    [MaxLength(100)]
    public string? City { get; set; }

    [MaxLength(50)]
    public string? State { get; set; }

        [MaxLength(50)]
    public string? Country { get; set; }

    [MaxLength(10)]
    [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip Code")] // Regex for zip code
    public string? ZipCode { get; set; }
}
}
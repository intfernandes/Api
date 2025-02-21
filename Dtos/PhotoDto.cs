
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class PhotoDto
    {
    public Guid Id { get; set; }
    [Required]
    [MaxLength(2048)]
    public string ImageUrl { get; set; } = null!;
    [MaxLength(2048)]
    public string Description { get; set; } = null!;
    public bool Highlight { get; set; }
    }
}
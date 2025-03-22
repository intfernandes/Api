
using System.ComponentModel.DataAnnotations;
using Api.Entities;

namespace Api.Dtos
{
    public class PhotoDto : BaseEntity
    {
    [Required]
    [MaxLength(2048)]
    public string ImageUrl { get; set; } = null!;
    [MaxLength(2048)]
    public string Description { get; set; } = null!;
    public bool Highlight { get; set; }
    }
}
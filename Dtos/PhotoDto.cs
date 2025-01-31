
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class PhotoDto
    {
    public int Id { get; set; }
    [Required]
    [MaxLength(2048)]
    public string Url { get; set; } = null!;
    public bool IsHighlight { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Dtos;


namespace Api.Entities
{
    [Table("Photos")]
    public class Photo : AuditableBaseEntity
    {
        [Required]
        [MaxLength(2048)]
        public string ImageUrl { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Highlight { get; set; }

        #region Relationships

        public Guid? UserId { get; set; }       
        public virtual IUser? User { get; set; }  

        public Guid? ProductId { get; set; } 
        public virtual Product? Product { get; set; }
        public Guid? DomainId { get; set; }
        public virtual Domain? Domain { get; set; }

        public Guid? CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        #endregion
    }
}
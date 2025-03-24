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

        public Guid? CustomerId { get; set; }       
        public virtual Customer? Customer { get; set; }  

        public Guid? EmployeeId { get; set; }       
        public virtual Employee? Employee { get; set; }  

        public Guid? ProductId { get; set; } 
        public virtual Product? Product { get; set; }
        public Guid? StoreId { get; set; }
        public virtual Store? Store { get; set; }

        public Guid? CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        #endregion
    }
}
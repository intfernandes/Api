using System.ComponentModel.DataAnnotations;


namespace Api.Entities
{
    public class Store : AuditableBaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public required string Email { get; set; } = string.Empty;
        [Required]
        public byte[] PasswordHash { get; set; } = [];
        [Required]
        public byte[] PasswordSalt { get; set; } = [];
        [MaxLength(500)]
        public string Description { get; set; } = String.Empty;

        #region Relationships

        [Required]
        public Guid AddressId { get; set; }
        public virtual Address? Address { get; set; } 
        [Required]
        public Guid AccountId { get; set; }
        public virtual Account? Account { get; set; } 
        public virtual ICollection<Employee> Employees { get; set; } = [];
        public virtual ICollection<Product> Products { get; set; } = [];
        public virtual ICollection<Order> Orders { get; set; } = [];   
        public virtual ICollection<Category> Categories { get; set; } = [];
        public List<Photo> Photos { get; set; } = []; 

        #endregion
    }
}
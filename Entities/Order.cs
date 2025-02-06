using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Entities.Users;

namespace Api.Entities
{     
        public class Order : AuditableBaseEntity
    {
        public DateTime DeliveryTerm { get; set; } = DateTime.UtcNow.Add(TimeSpan.FromHours(1));
        [Column(TypeName = "nvarchar(24)")]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [MaxLength(500)]
        public string? Notes { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        #region Relationships
        [Required]
        // Replaced CustomerId, Customer with generic IUser OrderPlacer:
        public required Guid CustomerId { get; set; } // Renamed from CustomerId to OrderPlacerId - FK to IUser who placed the order
        public virtual IUser Customer { get; set; } = null!; // Renamed from Customer to OrderPlacer - Navigation to IUser who placed the order

        [Required]
        public required Guid MemberId { get; set; }
        public virtual Member Member { get; set; } = null!;
        [Required]
        public Guid CompanyId { get; set; }
        public virtual Company? Company { get; set; }
        public Guid? ShippingAddressId { get; set; }
        public Guid? BillingAddressId { get; set; }
        [MaxLength(255)]
        public virtual Address? ShippingAddress { get; set; }
        [MaxLength(255)]
        public virtual Address? BillingAddress { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; } = [];

        #endregion

        public override string ToString()
        {
            return $"Order ID: {Id}, Customer: {Customer?.FirstName}, Seller: {Member?.FirstName}, DeliveryTerm: {DeliveryTerm}"; // Updated ToString to use OrderPlacer
        }
    }
    



    public class OrderItem : BaseEntity // Inherits from BaseEntity - ID is now inherited!
    {
        // Removed explicit 'public Guid Id { get; set; } = Guid.NewGuid();' - Inherited from BaseEntity

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        #region Relationships
        [Required]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;
        [Required]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
        #endregion
    }

}
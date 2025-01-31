
using System.ComponentModel.DataAnnotations;
using Api.Entities.Users;

namespace Api.Entities
{
public class Order : AuditableBaseEntity
{
    public DateTime DeliveryTerm { get; set; } = DateTime.UtcNow.Add(TimeSpan.FromHours(1));

    public OrderStatus Status { get; set; } = OrderStatus.Pending;  

    [MaxLength(500)]
    public string? Notes { get; set; }

    public virtual List<OrderItem> OrderItems { get; set; } = [];  

    [Range(0, double.MaxValue)]
    public decimal TotalAmount { get; set; }

    #region Relationships
    [Required]
    public required int CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;  
    [Required]
    public required int MemberId { get; set; }  
    public virtual Member Member { get; set; } = null!;
    [Required]
    public int? CompanyId { get; set; }
    public virtual Company? Company { get; set; }
    public int? ShippingAddressId { get; set; } 
    public int? BillingAddressId { get; set; } 
    [MaxLength(255)]
    public required Address ShippingAddress { get; set; }
    [MaxLength(255)]
    public required Address BillingAddress { get; set; }
    #endregion

    public override string ToString()
    {
        return $"Order ID: {Id}, Customer: {Customer?.FirstName}, Seller: {Member?.FirstName}, DeliveryTerm: {DeliveryTerm}";
    }
}

public class OrderItem 
{   
    public Guid Id { get; set; } = Guid.NewGuid();

    [Range(1, int.MaxValue)] 
    public int Quantity { get; set; }
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }  

    #region Relationships
    public int OrderId { get; set; }
    public virtual Order Order { get; set; } = null!;  
    public int ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;  
    #endregion

}
}
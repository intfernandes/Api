
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Api.Entities;

namespace Api.Dtos
{
  public class OrderDto : BaseEntity
{

    [Required]
    public Guid CustomerId { get; set; } 
    [Required]
    public Guid MemberId { get; set; } 
    public DateTime OrderDate { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter<OrderStatus>))]
    public OrderStatus Status { get; set; }
    [MaxLength(500)]
    public string? Notes { get; set; }
    public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    [MaxLength(255)]
    public string? ShippingAddress { get; set; }
    [MaxLength(255)]
    public string? BillingAddress { get; set; }
    [Range(0, double.MaxValue)]
    public decimal TotalAmount { get; set; }

}

public class OrderItemDto : BaseEntity
{
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
}
}
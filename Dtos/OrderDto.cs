
using System.ComponentModel.DataAnnotations;
using Api.Entities;

namespace Api.Dtos
{
  public class OrderDto
{
    public int Id { get; set; }

    [Required]
    public int CustomerId { get; set; }

    public int? SellerId { get; set; }

    public DateTime OrderDate { get; set; }

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

public class OrderItemDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
}
}
namespace Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }
    
    // Payload
    public int Quantity { get; set; }
    public decimal PriceAtPurchase { get; set; } // CRITICAL: Price at the time of order

    // Foreign Keys
    public int OrderId { get; set; }
    public int ProductId { get; set; }

    // Navigation Properties
    public Order Order { get; set; }
    public Product Product { get; set; }
}
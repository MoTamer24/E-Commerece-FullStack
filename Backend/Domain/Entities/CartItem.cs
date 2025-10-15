namespace Domain.Entities;

public class CartItem
{
    public int Id { get; set; } 
    
    // Payload
    public int Quantity { get; set; }

    // Foreign Keys
    public int CartId { get; set; }
    public int ProductId { get; set; }

    // Navigation Properties
    public Cart Cart { get; set; }
    public Product Product { get; set; }
}
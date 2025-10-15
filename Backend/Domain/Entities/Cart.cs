namespace Domain.Entities;
using Domain.Entities.Identity;
public class Cart
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    // Foreign Key (Can be nullable if you support anonymous users)
    public string UserId { get; set; }

    // Navigation Properties
    public ApplicationUser User { get; set; }
    public ICollection<CartItem> CartItems { get; set; } // Links to the items in the cart
}
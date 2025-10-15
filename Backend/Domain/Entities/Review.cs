namespace Domain.Entities;
using Domain.Entities.Identity;
public class Review
{
    public int Id { get; set; }
    public int Rating { get; set; } // e.g., 1 to 5
    public string Comment { get; set; }
    public DateTime ReviewDate { get; set; } = DateTime.UtcNow;

    // Foreign Keys
    public int ProductId { get; set; }
    public string UserId { get; set; } // FK to ApplicationUser

    // Navigation Properties
    public Product Product { get; set; }
    public ApplicationUser User { get; set; }
}
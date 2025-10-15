namespace Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
    public string TransactionId { get; set; } // ID provided by the payment gateway (e.g., Stripe)
    public string PaymentMethod { get; set; } // e.g., "Credit Card", "PayPal"
    public string Status { get; set; } // e.g., "Successful", "Failed"

    // Foreign Key (1:1 relationship with Order)
    public int OrderId { get; set; }

    // Navigation Property
    public Order Order { get; set; }
}
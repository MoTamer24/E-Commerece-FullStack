namespace Application.DTOs;

/// <summary>
/// Represents details of a payment transaction.
/// </summary>
public class PaymentDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Status { get; set; } = string.Empty; // e.g., "Completed", "Failed"
}


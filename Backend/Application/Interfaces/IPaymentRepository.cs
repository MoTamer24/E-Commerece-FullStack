using Domain.Entities;

namespace Application.Interfaces;

public interface IPaymentRepository : IGenericRepository<Payment>
{
    // Custom methods might include finding a payment by transaction ID
    // Task<Payment?> GetByTransactionIdAsync(string transactionId);
}
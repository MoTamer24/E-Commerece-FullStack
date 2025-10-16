using Application.Interfaces;
using Application.Interfaces.Services;
using Application.DTOs;

namespace Infrastructure.Services;

// A simplified service for now.
public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<PaymentDto> GetPaymentDetailsForOrderAsync(int orderId)
    {
        // This assumes a simple 1-to-1 link between Order and Payment.
        var payment = await _paymentRepository.FindAsync(p => p.OrderId == orderId);
        if (payment == null) return null;

        return new PaymentDto
        {
            Id = payment.Id,
            Amount = payment.Amount,
            PaymentDate = payment.PaymentDate,
            Status = payment.Status
            
        };
    }
}
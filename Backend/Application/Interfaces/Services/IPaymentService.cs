using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IPaymentService
{
    Task<PaymentDto> GetPaymentDetailsForOrderAsync(int orderId);
    // In a real app, this would be more complex:
    // Task<ProcessPaymentResponseDto> ProcessPaymentAsync(ProcessPaymentRequestDto paymentDetails);
}
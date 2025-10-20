using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IPaymentService
{
    Task<PaymentDto> GetPaymentDetailsForOrderAsync(int orderId);
    Task<string> CreateOrUpdatePaymentIntent(int orderId);
    
}
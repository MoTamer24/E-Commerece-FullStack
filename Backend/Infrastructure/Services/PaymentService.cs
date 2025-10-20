using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Application.DTOs;

namespace Infrastructure.Services
{
    public class MockPaymentService : IPaymentService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository; 
        IPaymentRepository _paymentRepository;

        public MockPaymentService(IConfiguration config, IUnitOfWork unitOfWork, IOrderRepository orderRepository,
            IPaymentRepository paymentRepository)
        {
            _config = config;
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<string> CreateOrUpdatePaymentIntent(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId.ToString());
            if (order == null) throw new KeyNotFoundException("Order not found");

            return Guid.NewGuid().ToString();
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
}
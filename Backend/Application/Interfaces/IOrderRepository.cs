namespace Application.Interfaces;
using Domain.Entities;

public interface IOrderRepository : IGenericRepository<Order>
{
  Task<IEnumerable<Order>> GetOrdersByCustomerId(int customerId);
    public Task<Order> GetOrderDetails(int orderId);
}
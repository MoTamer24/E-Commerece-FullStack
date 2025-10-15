using Domain.Entities;

namespace Application.IRepositories;

public interface IOrderRepository:IGenericRepository<Order>
{
    public Task<IEnumerable<Order>> GetByUserId(int userId);
    
}
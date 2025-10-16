using Domain.Entities;

namespace Application.Interfaces;

public interface IReviewRepository : IGenericRepository<Review>
{
    // A useful custom method would be to get all reviews for a specific product.
    Task<IEnumerable<Review>> GetReviewsForProductAsync(int productId);
}
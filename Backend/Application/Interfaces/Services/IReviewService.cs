using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetReviewsForProductAsync(int productId);
    Task<ReviewDto> AddReviewAsync(CreateReviewDto reviewDto);
}
using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Infrastructure.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IProductRepository _productRepository; // We need this to validate the product exists

    public ReviewService(IReviewRepository reviewRepository, IProductRepository productRepository)
    {
        _reviewRepository = reviewRepository;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewsForProductAsync(int productId)
    {
        var reviews = await _reviewRepository.GetReviewsForProductAsync(productId);
        return reviews.Select(r => new ReviewDto 
        { 
            Id = r.Id,
            Rating = r.Rating,
            Comment = r.Comment,
            ReviewerName = "Anonymous" // Or get from Customer entity
        });
    }

    public async Task<ReviewDto> AddReviewAsync(CreateReviewDto reviewDto)
    {
        // Business Logic: Ensure the product exists before adding a review
        var product = await _productRepository.GetByIdAsync(reviewDto.ProductId.ToString());
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with ID {reviewDto.ProductId} not found.");
        }

        var reviewEntity = new Review
        {
            Rating = reviewDto.Rating,
            Comment = reviewDto.Comment,
            ProductId = reviewDto.ProductId
            // CustomerId would be set here
        };

        await _reviewRepository.AddAsync(reviewEntity);
        // await _unitOfWork.SaveChangesAsync();

        return new ReviewDto { Id = reviewEntity.Id, Rating = reviewEntity.Rating, Comment = reviewEntity.Comment };
    }
}
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        // Using IGenericRepository since Review doesn't have a custom one
        private readonly IGenericRepository<Review> _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReviewsController(IGenericRepository<Review> reviewRepository, IUnitOfWork unitOfWork)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/reviews/product/5
        [HttpGet("product/{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReviewsForProduct(int productId)
        {
            var allReviews = await _reviewRepository.GetAllAsync();
            var productReviews = allReviews.Where(r => r.ProductId == productId);
            return Ok(productReviews);
        }

        // POST: api/reviews
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateReview([FromBody] Review review)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            review.UserId = userId;

            await _reviewRepository.AddAsync(review);
            await _unitOfWork.SaveAllChangesAsync();
            return Ok(review);
        }

        // DELETE: api/reviews/10
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var review = await _reviewRepository.GetByIdAsync(id.ToString());

            if (review == null) return NotFound();

            if (review.UserId != userId)
            {
                return Forbid();
            }

            _reviewRepository.Remove(review);
            await _unitOfWork.SaveAllChangesAsync();
            return NoContent();
        }
    }
}


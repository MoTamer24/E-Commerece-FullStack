namespace Application.Interfaces.Services;
using Application.DTOs;
public interface ICartService
{
    Task<CartDto> GetCartAsync(string userId);
    Task AddToCartAsync(string userId, int productId, int quantity);
    Task RemoveFromCartAsync(string userId, int productId);
}
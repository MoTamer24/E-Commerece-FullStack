namespace Application.Interfaces.Services;
using Application.DTOs;
public interface ICartService
{
    Task<CartDto> GetCartAsync(int userId);
    Task AddToCartAsync(int userId, int productId, int quantity);
    Task RemoveFromCartAsync(int userId, int productId);
}
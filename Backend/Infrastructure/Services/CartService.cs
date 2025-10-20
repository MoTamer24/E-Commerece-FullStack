namespace Infrastructure.Services;
using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities;
using Application.DTOs;
// Infrastructure/Services/CartService.cs
public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _ProductRepository;
    private readonly IGenericRepository<CartItem> _cartItemRepository;


    public CartService
        (IUnitOfWork unitOfWork,ICartRepository cartRepository,
            IProductRepository productRepository , IGenericRepository<CartItem> cartItemRepository)
    {
        _unitOfWork = unitOfWork;
        _cartRepository = cartRepository;
        _ProductRepository=productRepository;
        _cartItemRepository = cartItemRepository;
    }

    public async Task AddToCartAsync(int userId, int productId, int quantity)
    {
        // 1. Get the user's cart (or create one if it doesn't exist)
        var cart = await _cartRepository.GetCartByCustomerId(userId);
        if (cart == null)
        {
            cart = new Cart { UserId = userId.ToString() };
            await _cartRepository.AddAsync(cart);
        }

        // 2. Check if the product is already in the cart
        var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);

        if (cartItem != null)
        {
            // If item exists, just update the quantity
            cartItem.Quantity += quantity;
        }
        else
        {
            // If not, add a new CartItem
            // You should also validate that the product exists and has stock
            var product = await _ProductRepository.GetByIdAsync(productId.ToString());
            if (product == null) throw new Exception("Product not found");

            cart.CartItems.Add(new CartItem { ProductId = productId, Quantity = quantity });
        }

        // 3. Save all changes in one transaction
        await _unitOfWork.SaveAllChangesAsync();
    }

    public async Task<CartDto> GetCartAsync(int userId)
    {
        var cart = await _cartRepository.GetCartByCustomerId(userId);
        if (cart == null)
        {
            return new CartDto(); // Return an empty cart
        }

        // Manual Mapping from Entity to DTO
        var cartDto = new CartDto
        {
            Items = cart.CartItems.Select(item => new CartItemDto
            {
                ProductId = item.ProductId,
                ProductName = item.Product.Name,
                Price = item.Product.Price,
                Quantity = item.Quantity
            }).ToList()
        };

        return cartDto;
    }

    public async Task RemoveFromCartAsync(int userId, int productId)
    {
        var cart = await _cartRepository.GetCartByCustomerId(userId);
        if (cart == null) return; 

        var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
        if (cartItem != null)
        {
            // 3. Use the repository to explicitly delete the entity
            _cartItemRepository.Remove(cartItem); // This tells EF to generate a DELETE statement
            await _unitOfWork.SaveAllChangesAsync();
        }
    }
}
using Domain.Entities;
using Application.DTOs;

namespace Application.Interfaces;

public interface IProductRepository:IGenericRepository<Product>
{
    public Task<IEnumerable<Product>> GetProductsByCategory(int categoryId);// Filter the catalog by a specific category.
    Task<IEnumerable<Product>> GetProductsById(List<int> ids);
}
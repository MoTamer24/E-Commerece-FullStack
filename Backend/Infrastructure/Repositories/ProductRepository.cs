using Application.Interfaces;
using Application.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product> ,IProductRepository 
{


    public ProductRepository(ApplicationDbContext context):base(context)
    {
       
    }
    public async Task<IEnumerable<Product>> GetProductsByCategory(int categoryId)
    {
     var pros= await  _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
    
     return pros; 
    }

    public async Task<Product> GetProductById(int id)
    {
        var res = await _context.Products
            .Include(p=>p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
        return res;
    }
    public async Task<IEnumerable<Product>> GetProductsById(List<int> ids)
    {
        var idSet = new HashSet<int>(ids);
        var products = await _context.Products
            .Include(p => p.Category)
            .Where(p => idSet.Contains(p.Id))
            .ToListAsync();
        
        return products;
    }
    

}
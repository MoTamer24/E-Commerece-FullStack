using Application.Interfaces;
using Application.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product> ,IProductRepository 
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context):base(context)
    {
        _context = context;
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
    

}
using Application.Interfaces;
using Domain.Entities;
using Infrastructure; // Assuming your DbContext is here

namespace Infrastructure.Repositories;

// Inherits the full implementation of all generic methods from GenericRepository
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    // We only need to implement custom methods defined in ICategoryRepository here.
    // For now, it's empty because the generic implementation is enough.
}
using Domain.Entities;


namespace Application.Interfaces;

// This interface inherits all the basic CRUD methods from IGenericRepository
// We can add category-specific database methods here if we ever need them.
public interface ICategoryRepository : IGenericRepository<Category>
{

}
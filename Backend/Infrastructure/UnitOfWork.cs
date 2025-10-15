using Application.IRepositories;

namespace Infrastructure;

public class UnitOfWork: IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveAllChangesAsync()
    {
         await _context.SaveChangesAsync();
    }
    
}
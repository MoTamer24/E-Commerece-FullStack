namespace Application.IRepositories;

public interface IUnitOfWork
{
    public Task SaveAllChangesAsync();
}
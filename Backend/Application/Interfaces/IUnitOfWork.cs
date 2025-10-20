namespace Application.Interfaces;

public interface IUnitOfWork
{
    public Task SaveAllChangesAsync();
}
namespace Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        public Task SaveChangesAsync();
    }
}

namespace Application.Interfaces.Repositories.BaseRepositories
{
    public interface IExistsRepository
    {
        Task<bool> ExistsAsync(Guid entity);
    }
}

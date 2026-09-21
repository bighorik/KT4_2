namespace Application.Interfaces.Repositories.BaseRepositories
{
    public interface ICreateRepository<T>
    {
        Task<T> CreateAsync(T entity);
    }
}

namespace Application.Interfaces.Repositories.BaseRepositories
{
    public interface IDeleteRepository<T>
    {
        Task DeleteAsync(T entity);
    }
}

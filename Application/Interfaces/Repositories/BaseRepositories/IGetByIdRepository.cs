namespace Application.Interfaces.Repositories.BaseRepositories
{
    public interface IGetByIdRepository<T>
    {
        Task<T?> GetByIdAsync(Guid id);
    }
}

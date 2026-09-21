namespace Application.Interfaces.Repositories.BaseRepositories
{
    public interface IGetFullIncludedByIdRepository<T>
    {
        Task<T?> GetFullIncludedByIdAsync(Guid id);
    }
}

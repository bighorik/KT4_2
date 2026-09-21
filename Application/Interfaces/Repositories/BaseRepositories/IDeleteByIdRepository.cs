namespace Application.Interfaces.Repositories.BaseRepositories
{
    public interface IDeleteByIdRepository
    {
        Task DeleteByIdAsync(Guid id);
    }
}

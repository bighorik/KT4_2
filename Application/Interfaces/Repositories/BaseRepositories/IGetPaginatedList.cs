using Application.Utils;

namespace Application.Interfaces.Repositories.BaseRepositories
{
    public interface IGetPaginatedList<T>
    {
        Task<PaginationContract<T>> GetPaginatedListAsync(PaginationQuery query);
    }
    public interface IGetPaginatedList<TEntity, TQuery> where TQuery : PaginationQuery
    {
        Task<PaginationContract<TEntity>> GetPaginatedListAsync(TQuery query);
    }
}

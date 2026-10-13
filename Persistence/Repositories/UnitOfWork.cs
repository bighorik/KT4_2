using Application.Interfaces.Repositories;

namespace Persistence.Repositories
{
    public class UnitOfWork(
        ApplicationContext context) : IUnitOfWork
    {
        public Task SaveChangesAsync()
         => context.SaveChangesAsync();
    }
}

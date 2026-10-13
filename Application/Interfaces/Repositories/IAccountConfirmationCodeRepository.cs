using Application.Interfaces.Repositories.BaseRepositories;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IAccountConfirmationCodeRepository :
        ICreateRepository<AccountConfirmationCodeEntity>
    {
        public Task<AccountConfirmationCodeEntity?> GetByEmailAsync(string email);
        public Task<AccountConfirmationCodeEntity?> GetCodeByCodeAsync(string code);
        public Task MarkCodeAsUsedAsync(Guid Id);
    }
}

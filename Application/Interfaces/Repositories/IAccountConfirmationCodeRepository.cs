using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IAccountConfirmationCodeRepository
    {
        public Task<AccountConfirmationCodeEntity> CreateAsync(AccountConfirmationCodeEntity code);
        public Task<AccountConfirmationCodeEntity?> GetByEmailAsync(string email);
        public Task<AccountConfirmationCodeEntity?> GetCodeByCodeAsync(string code);
        public Task MarkCodeAsUsedAsync(Guid Id);
    }
}

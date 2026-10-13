using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class AccountConfirmationCodeRepository(
        ApplicationContext context) : IAccountConfirmationCodeRepository
    {
        public async Task<AccountConfirmationCodeEntity> CreateAsync(AccountConfirmationCodeEntity entity)
        {
            await context.AccountConfirmationCodes.AddAsync(entity);
            return entity;
        }

        public Task<AccountConfirmationCodeEntity?> GetByEmailAsync(string email)
        {
            return context.AccountConfirmationCodes.FirstOrDefaultAsync(codes => codes.User.Email == email);
        }

        public Task<AccountConfirmationCodeEntity?> GetCodeByCodeAsync(string code)
        {
            return context.AccountConfirmationCodes.FirstOrDefaultAsync(codes => codes.Code == code);
        }

        public async Task MarkCodeAsUsedAsync(Guid Id)
        {
            AccountConfirmationCodeEntity code
                = await context.AccountConfirmationCodes.SingleAsync(codes => codes.Id == Id);

            code.ConsumedAt = DateTime.UtcNow;
        }
    }
}


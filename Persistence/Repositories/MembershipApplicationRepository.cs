using Application.Behaviour.MembershipApplication;
using Application.Behaviour.Project;
using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class MembershipApplicationRepository(
        ApplicationContext context) : IMembershipApplicationRepository
    {
        public async Task<MembershipApplicationEntity> CreateAsync(MembershipApplicationEntity entity)
        {
            await context.MembershipApplications.AddAsync(entity);

            return entity;
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return context.MembershipApplications.AnyAsync(membership => membership.Id == id);
        }

        public async Task<PaginationContract<MembershipApplicationEntity>> GetApplicationsWithProjectByUserAsync(GetUserMembershipApplicationsQuery query)
        {
            IQueryable<MembershipApplicationEntity> queryable
                = context.MembershipApplications
                         .Where(app => app.UserId == query.UserId)
                         .Where(app => app.Status == query.Status && query.Status != null);

            List<MembershipApplicationEntity> apps =
                await queryable.Skip(query.Offset)
                               .Take(query.Limit)
                               .Include(app => app.Position)
                               .ThenInclude(position => position.Project)
                               .ToListAsync();
            int count = await queryable.CountAsync();

            PaginationContract<MembershipApplicationEntity> result = new()
            {
                Limit = query.Limit,
                Total = count,
                Items = apps,
            };

            return result;
        }

        public async Task<PaginationContract<MembershipApplicationEntity>> GetApplicationsWithUserByProjectAsync(GetProjectMembershipApplicationsQuery query)
        {
            List<MembershipApplicationEntity> apps =
                await context.MembershipApplications
                             .Where(app => app.Position.ProjectId == query.ProjectId)
                             .Where(app => app.Status == query.Status && query.Status != null)
                             .Skip(query.Offset)
                             .Take(query.Limit)
                             .Include(app => app.User)
                             .ToListAsync();
            int count = await context.MembershipApplications.CountAsync();

            PaginationContract<MembershipApplicationEntity> result = new()
            {
                Limit = query.Limit,
                Total = count,
                Items = apps,
            };

            return result;
        }

        public Task<MembershipApplicationEntity?> GetByIdAsync(Guid id)
        {
            return context.MembershipApplications.FirstOrDefaultAsync(app => app.Id == id);
        }

        public Task<MembershipApplicationEntity?> GetFullIncludedByIdAsync(Guid id)
        {
            return context.MembershipApplications
                          .Where(app => app.Id == id)
                          .Include(app => app.User)
                          .Include(app => app.Position)
                          .ThenInclude(position => position.Project)
                          .FirstOrDefaultAsync();
        }

        public Task<int> GetUserProjectsMemberApplicationsCountAsync(Guid userId)
        {
            return context.MembershipApplications.Where(app => app.Position.Project.OwnerId == userId).CountAsync();
        }

        public async Task<MembershipApplicationEntity> UpdateMembershipStatusAsync(Guid id, MembershipApplicationStatus status)
        {
            await context.MembershipApplications
                         .Where(app => app.Id == id)
                         .ExecuteUpdateAsync(set => set.SetProperty(app => app.Status, app => status));

            return await context.MembershipApplications.FirstAsync(app => app.Id == id);
        }
    }
}

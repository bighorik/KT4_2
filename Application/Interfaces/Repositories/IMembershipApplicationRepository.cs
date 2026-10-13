using Application.Behaviour.MembershipApplication;
using Application.Interfaces.Repositories.BaseRepositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface IMembershipApplicationRepository :
        ICreateRepository<MembershipApplicationEntity>,
        IGetByIdRepository<MembershipApplicationEntity>,
        IGetFullIncludedByIdRepository<MembershipApplicationEntity>,
        IExistsRepository
    {
        Task<int> GetUserProjectsMemberApplicationsCountAsync(Guid userId);
        Task<PaginationContract<MembershipApplicationEntity>> GetApplicationsWithUserByProjectAsync(GetProjectMembershipApplicationsQuery query);
        Task<PaginationContract<MembershipApplicationEntity>> GetApplicationsWithProjectByUserAsync(GetUserMembershipApplicationsQuery query);
        Task<MembershipApplicationEntity> UpdateMembershipStatusAsync(Guid id, MembershipApplicationStatus status);
    }
}

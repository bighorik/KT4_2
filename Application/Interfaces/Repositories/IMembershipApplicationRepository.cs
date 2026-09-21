using Application.Behaviour.MembershipApplication;
using Application.Interfaces.Repositories.BaseRepositories;
using Application.Utils;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IMembershipApplicationRepository :
        ICreateRepository<MembershipApplicationEntity>,
        IGetByIdRepository<MembershipApplicationEntity>,
        IGetFullIncludedByIdRepository<MembershipApplicationEntity>
    {
        Task<PaginationContract<MembershipApplicationEntity>> GetApplicationsWithUserByProjectAsync(GetProjectMembershipApplicationsQuery query);
        Task<PaginationContract<MembershipApplicationEntity>> GetApplicationsWithProjectByUserAsync(GetUserMembershipApplicationsQuery query);
    }
}

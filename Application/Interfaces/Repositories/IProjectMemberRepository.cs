using Application.Behaviour.Project;
using Application.Interfaces.Repositories.BaseRepositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Interfaces.Repositories
{
    public interface IProjectMemberRepository :
        IGetFullIncludedByIdRepository<ProjectMemberEntity>,
        IDeleteRepository<ProjectMemberEntity>
    {
    }
}
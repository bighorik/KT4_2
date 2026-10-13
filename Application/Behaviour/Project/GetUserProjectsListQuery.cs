using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Project
{
    public class GetUserProjectsListQuery : PaginationQuery, IRequest<PaginationContract<UserProjectModel>>
    {
        public Guid UserId { get; set; }
    }

    public class GetUserProjectsListQueryHandler(
        IProjectRepository projectRepository) : IRequestHandler<GetUserProjectsListQuery, PaginationContract<UserProjectModel>>
    {
        Task<PaginationContract<UserProjectModel>> IRequestHandler<GetUserProjectsListQuery, PaginationContract<UserProjectModel>>.Handle(GetUserProjectsListQuery request, CancellationToken cancellationToken)
        {
            return projectRepository.GetPaginatedListAsync(request);
        }
    }

    public class GetUserProjectsListQueryValidator : BasePaginationValidator<GetUserProjectsListQuery>
    {
        public GetUserProjectsListQueryValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
        }
    }
}

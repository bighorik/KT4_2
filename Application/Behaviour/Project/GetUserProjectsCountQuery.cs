using Application.Interfaces.Repositories;
using Application.Utils;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Project
{
    public record GetUserProjectsCountQuery : IRequest<ResponseContract<int>>
    {
        public required Guid UserId { get; set; }
    }

    public class GetUserProjectsCountQueryHandler(
        IProjectRepository projectRepository) : IRequestHandler<GetUserProjectsCountQuery, ResponseContract<int>>
    {
        public async Task<ResponseContract<int>> Handle(GetUserProjectsCountQuery request, CancellationToken cancellationToken)
        {
            int count = await projectRepository.GetUserProjectsCountAsync(request.UserId);

            return new ResponseContract<int>(count);
        }
    }

    public class GetUserProjectsCountQueryValidator : AbstractValidator<GetUserProjectsCountQuery>
    {
        public GetUserProjectsCountQueryValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
        }
    }
}

using Application.Interfaces.Repositories;
using Application.Utils;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Project
{
    public record GetUserProjectsMemberApplicationsCountQuery : IRequest<ResponseContract<int>>
    {
        public required Guid UserId { get; set; }
    }

    public class GetUserProjectsMemberApplicationsCountQueryHandler(
        IMembershipApplicationRepository applicationRepository) : IRequestHandler<GetUserProjectsMemberApplicationsCountQuery, ResponseContract<int>>
    {
        public async Task<ResponseContract<int>> Handle(GetUserProjectsMemberApplicationsCountQuery request, CancellationToken cancellationToken)
        {
            int count = await applicationRepository.GetUserProjectsMemberApplicationsCountAsync(request.UserId);

            return new ResponseContract<int>(count);
        }
    }

    public class GetUserProjectsMemberApplicationsCountQueryValidator : AbstractValidator<GetUserProjectsMemberApplicationsCountQuery>
    {
        public GetUserProjectsMemberApplicationsCountQueryValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
        }
    }
}

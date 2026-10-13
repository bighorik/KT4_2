using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Skill
{
    public class GetSkillsQuery : PaginationQuery, IRequest<PaginationContract<SkillEntity>>
    {
        public string? Name { get; set; }
    }

    public class GetSkillsQueryHandler(
        ISkillRepository skillRepository) : IRequestHandler<GetSkillsQuery, PaginationContract<SkillEntity>>
    {
        public Task<PaginationContract<SkillEntity>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
        {
            return skillRepository.GetPaginatedListAsync(request);
        }
    }

    public class GetSkillsQueryValidator : BasePaginationValidator<GetSkillsQuery>
    {
        public GetSkillsQueryValidator()
        {
            RuleFor(command => command.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(command => command.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
        }
    }
}

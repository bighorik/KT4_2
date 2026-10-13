using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Skill
{
    public class GetStudentGroupNumbersQuery : PaginationQuery, IRequest<PaginationContract<StudentGroupNumberEntity>>
    {
        public string? Name { get; set; }
    }

    public class GetStudentGroupNumbersQueryHandler(
        IStudentGroupNumberRepository groupRepository) : IRequestHandler<GetStudentGroupNumbersQuery, PaginationContract<StudentGroupNumberEntity>>
    {
        public Task<PaginationContract<StudentGroupNumberEntity>> Handle(GetStudentGroupNumbersQuery request, CancellationToken cancellationToken)
        {
            return groupRepository.GetPaginatedListAsync(request);
        }
    }

    public class GetStudentGroupNumbersQueryValidator : BasePaginationValidator<GetStudentGroupNumbersQuery>
    {
        public GetStudentGroupNumbersQueryValidator()
        {
            RuleFor(command => command.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(command => command.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
        }
    }
}

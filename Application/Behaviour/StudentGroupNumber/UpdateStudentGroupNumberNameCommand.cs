using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Skill
{
    public class UpdateStudentGroupNumberNameCommand : IRequest<ResponseContract<StudentGroupNumberEntity>>
    {
        public required Guid GroupId { get; set; }
        public required string Name { get; set; }
    }

    public class UpdateStudentGroupNumberNameCommandHandler(
        IStudentGroupNumberRepository groupRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateStudentGroupNumberNameCommand, ResponseContract<StudentGroupNumberEntity>>
    {
        public async Task<ResponseContract<StudentGroupNumberEntity>> Handle(UpdateStudentGroupNumberNameCommand request, CancellationToken cancellationToken)
        {
            if (!await groupRepository.ExistsAsync(request.GroupId))
                return new ResponseContract<StudentGroupNumberEntity>(ErrorCodes.StudentGroupNumberNotFound);

            StudentGroupNumberEntity group = await groupRepository.UpdateGroupNumberAsync(request.GroupId, request.Name);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract<StudentGroupNumberEntity>(group);
        }
    }

    public class UpdateStudentGroupNumberNameCommandValidator : AbstractValidator<UpdateStudentGroupNumberNameCommand>
    {
        public UpdateStudentGroupNumberNameCommandValidator()
        {
            RuleFor(command => command.GroupId).NotEmpty();
            RuleFor(command => command.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
        }
    }
}

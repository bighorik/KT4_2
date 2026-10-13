using Application.Interfaces.Repositories;
using Application.Utils;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.ProjectPosition
{
    public class DeleteStudentGroupNumberCommand : IRequest<ResponseContract>
    {
        public required Guid GroupId { get; set; }
    }

    public class DeleteStudentGroupNumberCommandHandler(
            ISkillRepository skillRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<DeleteStudentGroupNumberCommand, ResponseContract>
    {
        public async Task<ResponseContract> Handle(DeleteStudentGroupNumberCommand request, CancellationToken cancellationToken)
        {
            await skillRepository.DeleteByIdAsync(request.GroupId);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract();
        }
    }

    public class DeleteStudentGroupNumberCommandValidator : AbstractValidator<DeleteStudentGroupNumberCommand>
    {
        public DeleteStudentGroupNumberCommandValidator()
        {
            RuleFor(command => command.GroupId).NotEmpty();
        }
    }
}

using Application.Interfaces.Repositories;
using Application.Utils;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.ProjectPosition
{
    public class DeleteProjectPositionCommand : IRequest<ResponseContract>
    {
        public required Guid ProjectPositionId { get; set; }
    }

    public class DeleteProjectPositionCommandHandler(
            IProjectPositionRepository positionRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<DeleteProjectPositionCommand, ResponseContract>
    {
        public async Task<ResponseContract> Handle(DeleteProjectPositionCommand request, CancellationToken cancellationToken)
        {
            if (!await positionRepository.ExistsAsync(request.ProjectPositionId))
                return new ResponseContract(ErrorCodes.ProjectPositionNotFound);

            await positionRepository.DeleteByIdAsync(request.ProjectPositionId);

            await unitOfWork.SaveChangesAsync();

            return new ResponseContract();
        }
    }

    public class DeleteProjectPositionCommandValidator : AbstractValidator<DeleteProjectPositionCommand>
    {
        public DeleteProjectPositionCommandValidator()
        {
            RuleFor(command => command.ProjectPositionId).NotEmpty();
        }
    }
}

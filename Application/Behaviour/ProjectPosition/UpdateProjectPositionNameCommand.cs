using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.ProjectPosition
{
    public class UpdateProjectPositionNameCommand : IRequest<ResponseContract<ProjectPositionEntity>>
    {
        public required Guid ProjectPositionId { get; set; }
        public required string Name { get; set; }
    }

    public class UpdateProjectPositionNameCommandHandler(
            IProjectPositionRepository positionRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<UpdateProjectPositionNameCommand, ResponseContract<ProjectPositionEntity>>
    {
        public async Task<ResponseContract<ProjectPositionEntity>> Handle(UpdateProjectPositionNameCommand request, CancellationToken cancellationToken)
        {
            if (!await positionRepository.ExistsAsync(request.ProjectPositionId))
                return new ResponseContract<ProjectPositionEntity>(ErrorCodes.ProjectPositionNotFound);

            ProjectPositionEntity position = await positionRepository.UpdateNameAsync(request.ProjectPositionId, request.Name);

            await unitOfWork.SaveChangesAsync();

            return new ResponseContract<ProjectPositionEntity>(position);
        }
    }

    public class UpdateProjectPositionNameCommandValidator : AbstractValidator<UpdateProjectPositionNameCommand>
    {
        public UpdateProjectPositionNameCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(command => command.ProjectPositionId).NotEmpty();
        }
    }
}

using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.ProjectPosition
{
    public class UpdateProjectPositionDescriptionCommand : IRequest<ResponseContract<ProjectPositionEntity>>
    {
        public required Guid ProjectPositionId { get; set; }
        public required string Description { get; set; }
    }

    public class UpdateProjectPositionDescriptionCommandHandler(
            IProjectPositionRepository positionRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<UpdateProjectPositionDescriptionCommand, ResponseContract<ProjectPositionEntity>>
    {
        public async Task<ResponseContract<ProjectPositionEntity>> Handle(UpdateProjectPositionDescriptionCommand request, CancellationToken cancellationToken)
        {
            if (!await positionRepository.ExistsAsync(request.ProjectPositionId))
                return new ResponseContract<ProjectPositionEntity>(ErrorCodes.ProjectPositionNotFound);

            ProjectPositionEntity position = await positionRepository.UpdateDescriptionAsync(request.ProjectPositionId, request.Description);

            await unitOfWork.SaveChangesAsync();

            return new ResponseContract<ProjectPositionEntity>(position);
        }
    }

    public class UpdateProjectPositionDescriptionCommandValidator : AbstractValidator<UpdateProjectPositionDescriptionCommand>
    {
        public UpdateProjectPositionDescriptionCommandValidator()
        {
            RuleFor(command => command.Description).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(command => command.ProjectPositionId).NotEmpty();
        }
    }
}

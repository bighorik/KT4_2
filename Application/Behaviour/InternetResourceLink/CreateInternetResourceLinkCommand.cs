using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.InternetResourceLink
{
    public class CreateInternetResourceLinkCommand : IRequest<ResponseContract<InternetResourceLinkEntity>>
    {
        public Guid ProjectId { get; set; }
    }

    public class CreateInternetResourceLinkCommandHandler(
        IProjectRepository projectRepository,
        IInternetResourceLinksRepository linkRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<CreateInternetResourceLinkCommand, ResponseContract<InternetResourceLinkEntity>>
    {
        public async Task<ResponseContract<InternetResourceLinkEntity>> Handle(CreateInternetResourceLinkCommand request, CancellationToken cancellationToken)
        {
            if (!await projectRepository.ExistsAsync(request.ProjectId))
                return new ResponseContract<InternetResourceLinkEntity>(ErrorCodes.ProjectNotFound);

            InternetResourceLinkEntity link = new()
            {
                Id = Guid.NewGuid(),
                Url = "",
                Type = InternetResourceLinkType.Telegram,
                ProjectId = request.ProjectId,
            };

            await linkRepository.CreateAsync(link);
            await unitOfWork.SaveChangesAsync();

            return new ResponseContract<InternetResourceLinkEntity>(link);
        }
    }

    public class CreateInternetResourceLinkCommandValidator : AbstractValidator<CreateInternetResourceLinkCommand>
    {
        public CreateInternetResourceLinkCommandValidator()
        {
            RuleFor(command => command.ProjectId).NotEmpty();
        }
    }
}
using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.InternetResourceLink
{
    public class UpdateInternetResourceLinkTypeCommand : IRequest<ResponseContract<InternetResourceLinkEntity>>
    {
        public required InternetResourceLinkType Type { get; set; }
        public Guid LinkId { get; set; }
    }

    public class UpdateInternetResourceLinkTypeCommandHandler(
        IInternetResourceLinksRepository linkRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<UpdateInternetResourceLinkTypeCommand, ResponseContract<InternetResourceLinkEntity>>
    {
        public async Task<ResponseContract<InternetResourceLinkEntity>> Handle(UpdateInternetResourceLinkTypeCommand request, CancellationToken cancellationToken)
        {
            if (!await linkRepository.ExistsAsync(request.LinkId))
                return new ResponseContract<InternetResourceLinkEntity>(ErrorCodes.InternetResourceLinkNotFound);

            InternetResourceLinkEntity link = await linkRepository.UpdateTypeAsync(request.LinkId, request.Type);
            await unitOfWork.SaveChangesAsync();

            return new ResponseContract<InternetResourceLinkEntity>(link);
        }
    }

    public class UpdateInternetResourceLinkTypeCommandValidator : AbstractValidator<UpdateInternetResourceLinkTypeCommand>
    {
        public UpdateInternetResourceLinkTypeCommandValidator()
        {
            RuleFor(command => command.LinkId).NotEmpty();
            RuleFor(command => command.Type).NotEmpty();
        }
    }
}
using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.InternetResourceLink
{
    public class UpdateInternetResourceLinkUrlCommand : IRequest<ResponseContract<InternetResourceLinkEntity>>
    {
        public required string Url { get; set; }
        public Guid LinkId { get; set; }
    }

    public class UpdateInternetResourceLinkUrlCommandHandler(
        IInternetResourceLinksRepository linkRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<UpdateInternetResourceLinkUrlCommand, ResponseContract<InternetResourceLinkEntity>>
    {
        public async Task<ResponseContract<InternetResourceLinkEntity>> Handle(UpdateInternetResourceLinkUrlCommand request, CancellationToken cancellationToken)
        {
            if (!await linkRepository.ExistsAsync(request.LinkId))
                return new ResponseContract<InternetResourceLinkEntity>(ErrorCodes.InternetResourceLinkNotFound);

            InternetResourceLinkEntity link = await linkRepository.UpdateUrlAsync(request.LinkId, request.Url);
            await unitOfWork.SaveChangesAsync();

            return new ResponseContract<InternetResourceLinkEntity>(link);
        }
    }

    public class UpdateInternetResourceLinkUrlCommandValidator : AbstractValidator<UpdateInternetResourceLinkUrlCommand>
    {
        public UpdateInternetResourceLinkUrlCommandValidator()
        {
            RuleFor(command => command.LinkId).NotEmpty();
            RuleFor(command => command.Url).NotEmpty().Url();
        }
    }
}
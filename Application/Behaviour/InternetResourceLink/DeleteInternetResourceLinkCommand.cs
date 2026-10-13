using Application.Interfaces.Repositories;
using Application.Utils;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.InternetResourceLink
{
    public class DeleteInternetResourceLinkCommand : IRequest<ResponseContract>
    {
        public Guid LinkId { get; set; }
    }

    public class DeleteInternetResourceLinkCommandHandler(
        IInternetResourceLinksRepository linkRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<DeleteInternetResourceLinkCommand, ResponseContract>
    {
        public async Task<ResponseContract> Handle(DeleteInternetResourceLinkCommand request, CancellationToken cancellationToken)
        {
            await linkRepository.DeleteByIdAsync(request.LinkId);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract();
        }
    }

    public class DeleteInternetResourceLinkCommandValidator : AbstractValidator<DeleteInternetResourceLinkCommand>
    {
        public DeleteInternetResourceLinkCommandValidator()
        {
            RuleFor(command => command.LinkId).NotEmpty();
        }
    }
}
using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Behaviour.Attachment
{
    public class CreateAttachmentCommand : IRequest<ResponseContract<AttachmentEntity>>
    {
        public required IFormFile File { get; set; }
        public required Guid UserId { get; set; }
        public required AttachmentType AttachmentType { get; set; }
    }

    public class CreateAttachmentCommandHandler(
        IAttachmentsRepository attachmentsRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateAttachmentCommand, ResponseContract<AttachmentEntity>>
    {
        public async Task<ResponseContract<AttachmentEntity>> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
        {
            AttachmentEntity attachmentResult = await attachmentsRepository.CreateAsync(request.File, request.UserId, request.AttachmentType);

            await unitOfWork.SaveChangesAsync();

            return new ResponseContract<AttachmentEntity>(attachmentResult);
        }
    }

    public class CreateAttachmentCommandValidator : AbstractValidator<CreateAttachmentCommand>
    {
        public CreateAttachmentCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.AttachmentType).NotEmpty();
        }
    }
}

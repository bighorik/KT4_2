using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Project
{
    public record CreateProjectCommand : IRequest<ResponseContract<ProjectEntity>>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required Guid OwnerId { get; set; }
        public List<CreateInternetResourceLink> InternetResourceLinks = [];
        public List<CreateProjectPosition> RequiredPositions = [];
        public List<Guid> AttachmentIds = [];
        public required bool IsAdditionalRecruitmentAllowed = false;
        public required bool IsStrictCandidatesSelection = false;

        public record CreateInternetResourceLink
        {
            public required InternetResourceLinkType Type { get; set; }
            public required string Url { get; set; }
        }

        public record CreateProjectPosition
        {
            public required string Name { get; set; }
            public List<CreateRequiredSkill> RequiredSkills { get; set; } = [];
        }

        public record CreateRequiredSkill
        {
            public Guid SkillId { get; set; }
            public SkillLevel SkillLevel { get; set; }
        }
    }

    public class CreateProjectCommandHandler(
        IProjectRepository projectRepository,
        IAttachmentsRepository attachmentsRepository,
        IProjectPositionRepository projectPositionRepository,
        ISkillRepository skillRepository,
        IUserRepository userRepository) : IRequestHandler<CreateProjectCommand, ResponseContract<ProjectEntity>>
    {
        public async Task<ResponseContract<ProjectEntity>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            if (!await userRepository.ExistsAsync(request.OwnerId))
                return new ResponseContract<ProjectEntity>(ErrorCodes.UserNotFound, request.OwnerId);

            foreach (Guid AttachmentId in request.AttachmentIds)
            {
                if (!await attachmentsRepository.ExistsAsync(AttachmentId))
                    return new ResponseContract<ProjectEntity>(ErrorCodes.AttachmentNotFound, AttachmentId);
            }

            List<Guid> requiredSkillIds = request.RequiredPositions
                                                 .SelectMany(position => position.RequiredSkills)
                                                 .Select(requiredSkill => requiredSkill.SkillId)
                                                 .Distinct()
                                                 .ToList();

            foreach (Guid requiredSkillId in requiredSkillIds)
            {
                if (!await skillRepository.ExistsAsync(requiredSkillId))
                    return new ResponseContract<ProjectEntity>(ErrorCodes.RequiredSkillNotFound, requiredSkillId);
            }

            List<InternetResourceLinkEntity> newLinks = 
                request.InternetResourceLinks
                        .Select(link =>  new InternetResourceLinkEntity()
                            {
                                Id = Guid.NewGuid(),
                                Type = link.Type,
                                Url = link.Url,
                            })
                        .ToList();

            List<ProjectPositionEntity> newPositions = [];
            foreach(CreateProjectCommand.CreateProjectPosition position in request.RequiredPositions)
            {
                ProjectPositionEntity newPosition = new ()
                {
                    Id = Guid.NewGuid(),
                    Name = position.Name,
                };

                foreach (CreateProjectCommand.CreateRequiredSkill requiredSkill in position.RequiredSkills)
                {
                    newPosition.RequiredSkills.Add(new()
                    {
                        Id = Guid.NewGuid(),
                        SkillLevel = requiredSkill.SkillLevel,
                        SkillId = requiredSkill.SkillId,
                    });
                }
                newPositions.Add(newPosition);
            }

            ProjectEntity project = new()
            { 
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                OwnerId = request.OwnerId,
                Attachments = [],
                InternetResourceLinks = newLinks,
                RequiredPositions = newPositions,
                IsAdditionalRecruitmentAllowed = request.IsAdditionalRecruitmentAllowed,
                IsStrictCandidatesSelection = request.IsStrictCandidatesSelection,
            };

            await projectRepository.AddProjectAsync(project);

            return project;
        }
    }

    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().MaximumLength(150);
            RuleFor(command => command.Description).NotEmpty().MaximumLength(500);
            RuleFor(command => command.OwnerId).NotEmpty();
            RuleFor(command => command.IsAdditionalRecruitmentAllowed).NotEmpty();
            RuleFor(command => command.IsStrictCandidatesSelection).NotEmpty();
            RuleFor(command => command.Name).NotEmpty().MaximumLength(50);
            RuleForEach(command => command.InternetResourceLinks).ChildRules(rules =>
            {
                rules.RuleFor(link => link.Type).NotEmpty();
                rules.RuleFor(link => link.Url).NotEmpty();
            });
            RuleForEach(command => command.RequiredPositions).ChildRules(rules =>
            {
                rules.RuleFor(command => command.Name).NotEmpty().MaximumLength(50);
            });
            RuleForEach(command => command.AttachmentIds).NotEmpty();
        }
    }
}

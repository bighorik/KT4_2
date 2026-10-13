using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Application.Utils;
using Application.Behaviour.Project;
using Application.Interfaces.Repositories;
using Domain.Models;
using Mapster;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Persistence.Repositories
{
    public class ProjectRepository(
            ApplicationContext context) : IProjectRepository
    {
        public async Task<ProjectEntity> AddAttachmentToProjectAsync(Guid id, Guid attachmentId)
        {
            ProjectEntity project = await context.Projects.FirstAsync(project => project.Id == id);
            AttachmentEntity attachment = await context.Attachments.FirstAsync(attachment => attachment.Id == attachmentId);
            project.Attachments.Add(attachment);

            return project;
        }

        public async Task<ProjectEntity> CreateAsync(ProjectEntity entity)
        {
            await context.Projects.AddAsync(entity);

            return entity;
        }

        public async Task<ProjectEntity> DeleteAttachmentFromProjectAsync(Guid id, Guid attachmentId)
        {
            ProjectEntity project = await context.Projects.FirstAsync(project => project.Id == id);
            project.Attachments.RemoveAll(attachment => attachment.Id == attachmentId);

            return project;
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return context.Projects.AnyAsync(project => project.Id == id);
        }

        public Task<ProjectEntity?> GetByIdAsync(Guid id)
        {
            return context.Projects.FirstOrDefaultAsync(project => project.Id == id);
        }

        public Task<UserProjectModel?> GetDetailedById(Guid id)
        {
            return context.Projects
                    .Select(project => new UserProjectModel()
                    {
                        Id = project.Id,
                        Description = project.Description,
                        IsAdditionalRecruitmentAllowed = project.IsAdditionalRecruitmentAllowed,
                        IsStrictCandidatesSelection = project.IsStrictCandidatesSelection,
                        Name = project.Name,
                        Status = project.Status,
                        InternetResourceLinks = project.InternetResourceLinks
                                                            .Select(link =>
                                                                new UserProjectInternetResourceLinkModel()
                                                                {
                                                                    Id = link.Id,
                                                                    Type = link.Type,
                                                                    Url = link.Url,
                                                                })
                                                            .ToList(),
                        MembershipApplicationsCount = project.MembershipApplications.Count(),
                        RequiredPositions = project.RequiredPositions
                                                        .Select(position =>
                                                            new UserProjectPositionModel()
                                                            {
                                                                Id = position.Id,
                                                                Description = position.Description,
                                                                Name = position.Name,
                                                                ProjectMember = position.ProjectMember != null ? new UserProjectMemberModel()
                                                                {
                                                                    Id = position.ProjectMember.Id,
                                                                    AvatarUrl = position.ProjectMember.User.AvatarUrl,
                                                                    Email = position.ProjectMember.User.Email,
                                                                    Nickname = position.ProjectMember.User.Nickname,
                                                                } : null,
                                                                PositionSkills = position.PositionSkills
                                                                                         .Select(skill =>
                                                                                            new UserProjectPositionSkillModel()
                                                                                            {
                                                                                                Id = skill.Id,
                                                                                                Name = skill.Skill.Name,
                                                                                                SkillId = skill.SkillId,
                                                                                                SkillLevel = skill.SkillLevel,
                                                                                            })
                                                                                         .ToList(),
                                                                MembershipApplications = project.MembershipApplications
                                                                                                .Select(app =>
                                                                                                    new UserProjectMembershipApplicationModel()
                                                                                                    {
                                                                                                        Id = app.Id,
                                                                                                        Message = app.Message,
                                                                                                        Status = app.Status,
                                                                                                        User = new UserProjectMemberModel()
                                                                                                        {
                                                                                                            Id = app.User.Id,
                                                                                                            AvatarUrl = app.User.AvatarUrl,
                                                                                                            Email = app.User.Email,
                                                                                                            Nickname = app.User.Nickname,
                                                                                                        },
                                                                                                    })
                                                                                                .ToList()


                                                            }).ToList()
                    })
                    .FirstOrDefaultAsync(project => project.Id == id);

        }

        public Task<ProjectEntity?> GetFullIncludedByIdAsync(Guid id)
        {
            return context.Projects
                          .Include(project => project.Owner)
                          //.Include(project => project.RequiredPositions)
                          //.Include(project => project.MembershipApplications)
                          //.Include(project => project.InternetResourceLinks)
                          //.Include(project => project.ProjectMembers)
                          .FirstOrDefaultAsync(project => project.Id == id);
        }

        public async Task<PaginationContract<ProjectEntity>> GetPaginatedListAsync(GetProjectListQuery query)
        {
            IQueryable<ProjectEntity> queryable
                    = context.Projects
                             .Where(project => query.Name == null || project.Name.Contains(query.Name))
                             .Where(project => project.Status == ProjectStatus.Recruiting || project.Status == ProjectStatus.RecruitingCompleted && query.IncludeRequitingCompletedProjects)
                             .Where(project => query.OnlyOwnersProjects == false || query.UserId == null || project.OwnerId == query.UserId);

            List<ProjectEntity> projects =
                   await queryable.Skip(query.Offset)
                                  .Take(query.Limit)
                                  .Include(project => project.RequiredPositions)
                                  .ToListAsync();

            int count = await queryable.CountAsync();

            PaginationContract<ProjectEntity> result = new()
            {
                Limit = query.Limit,
                Total = count,
                Items = projects,
            };

            return result;
        }

        public async Task<PaginationContract<UserProjectModel>> GetPaginatedListAsync(GetUserProjectsListQuery query)
        {
            IQueryable<UserProjectModel> queryable
                        = context.Projects
                                 .Where(project => project.OwnerId == query.UserId)

                                 .Select(project => new UserProjectModel()
                                 {
                                     Id = project.Id,
                                     Description = project.Description,
                                     IsAdditionalRecruitmentAllowed = project.IsAdditionalRecruitmentAllowed,
                                     IsStrictCandidatesSelection = project.IsStrictCandidatesSelection,
                                     Name = project.Name,
                                     Status = project.Status,
                                     InternetResourceLinks = project.InternetResourceLinks
                                                                        .Select(link =>
                                                                            new UserProjectInternetResourceLinkModel()
                                                                            {
                                                                                Id = link.Id,
                                                                                Type = link.Type,
                                                                                Url = link.Url,
                                                                            })
                                                                        .ToList(),
                                     MembershipApplicationsCount = project.MembershipApplications.Count(),
                                     RequiredPositions = project.RequiredPositions
                                                                    .Select(position =>
                                                                        new UserProjectPositionModel()
                                                                        {
                                                                            Id = position.Id,
                                                                            Description = position.Description,
                                                                            Name = position.Name,
                                                                            ProjectMember = position.ProjectMember != null ? new UserProjectMemberModel()
                                                                            {
                                                                                Id = position.ProjectMember.Id,
                                                                                AvatarUrl = position.ProjectMember.User.AvatarUrl,
                                                                                Email = position.ProjectMember.User.Email,
                                                                                Nickname = position.ProjectMember.User.Nickname,
                                                                            } : null,
                                                                            PositionSkills = position.PositionSkills
                                                                                                     .Select(skill =>
                                                                                                        new UserProjectPositionSkillModel()
                                                                                                        {
                                                                                                            Id = skill.Id,
                                                                                                            Name = skill.Skill.Name,
                                                                                                            SkillId = skill.SkillId,
                                                                                                            SkillLevel = skill.SkillLevel,
                                                                                                        })
                                                                                                     .ToList(),
                                                                            MembershipApplications = project.MembershipApplications
                                                                                                            .Select(app =>
                                                                                                                new UserProjectMembershipApplicationModel()
                                                                                                                {
                                                                                                                    Id = app.Id,
                                                                                                                    Message = app.Message,
                                                                                                                    Status = app.Status,
                                                                                                                    User = new UserProjectMemberModel()
                                                                                                                    {
                                                                                                                        Id = app.User.Id,
                                                                                                                        AvatarUrl = app.User.AvatarUrl,
                                                                                                                        Email = app.User.Email,
                                                                                                                        Nickname = app.User.Nickname,
                                                                                                                    },
                                                                                                                })
                                                                                                            .ToList()


                                                                        }).ToList()
                                 });

            List<UserProjectModel> projects =
                   await queryable.Skip(query.Offset)
                                  .Take(query.Limit)
                                  .ToListAsync();

            int count = await queryable.CountAsync();

            PaginationContract<UserProjectModel> result = new()
            {
                Limit = query.Limit,
                Total = count,
                Items = projects,
            };

            return result;
        }

        public Task<int> GetUserProjectsCountAsync(Guid userId)
        {
            return context.Projects.Where(project => project.OwnerId == userId).CountAsync();
        }

        public async Task<ProjectEntity> UpdateProjectAdditionalRecruitmentAllowAsync(Guid id, bool isAdditionalRecruitmentAllowed)
        {
            await context.Projects
                         .Where(project => project.Id == id)
                         .ExecuteUpdateAsync(set => set.SetProperty(project => project.IsAdditionalRecruitmentAllowed, isAdditionalRecruitmentAllowed));

            return await context.Projects.FirstAsync(project => project.Id == id);
        }

        public async Task<ProjectEntity> UpdateProjectDescriptionAsync(Guid id, string description)
        {
            await context.Projects
                         .Where(project => project.Id == id)
                         .ExecuteUpdateAsync(set => set.SetProperty(project => project.Description, description));

            return await context.Projects.FirstAsync(project => project.Id == id);
        }

        public async Task<ProjectEntity> UpdateProjectNameAsync(Guid id, string name)
        {
            await context.Projects
                         .Where(project => project.Id == id)
                         .ExecuteUpdateAsync(set => set.SetProperty(project => project.Name, name));

            return await context.Projects.FirstAsync(project => project.Id == id);
        }

        public async Task<ProjectEntity> UpdateProjectStatusAsync(Guid id, ProjectStatus status)
        {
            await context.Projects
                         .Where(project => project.Id == id)
                         .ExecuteUpdateAsync(set => set.SetProperty(project => project.Status, status));

            return await context.Projects.FirstAsync(project => project.Id == id);
        }

        public async Task<ProjectEntity> UpdateProjectStrictCandidatesSelectionAsync(Guid id, bool isStrictCandidatesSelection)
        {
            await context.Projects
                         .Where(project => project.Id == id)
                         .ExecuteUpdateAsync(set => set.SetProperty(project => project.IsStrictCandidatesSelection, isStrictCandidatesSelection));

            return await context.Projects.FirstAsync(project => project.Id == id);
        }
    }
}

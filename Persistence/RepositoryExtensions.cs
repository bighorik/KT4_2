using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.BaseRepositories;
using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Minio;
using Persistence.Repositories;

namespace Persistence
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAccountConfirmationCodeRepository, AccountConfirmationCodeRepository>();
            services.AddScoped<IAttachmentsRepository, AttachmentsRepository>();
            services.AddScoped<IInternetResourceLinksRepository, InternetResourceLinksRepository>();
            services.AddScoped<IMembershipApplicationRepository, MembershipApplicationRepository>();
            services.AddScoped<IPositionSkillRepository, PositionSkillRepository>();
            services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();
            services.AddScoped<IProjectPositionRepository, ProjectPositionRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IStorageRepository, StorageRepository>();
            services.AddScoped<IStudentGroupNumberRepository, StudentGroupNumberRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserContactRepository, UserContactRepository>();
            services.AddScoped<IUserSkillRepository, UserSkillRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddMinio(client =>
            {
                StorageSettings settings = configuration.GetStorageSettings();
                client.WithEndpoint(settings.Endpoint);
                client.WithCredentials(settings.AccessKey, settings.SecretKey);
                client.WithSSL(settings.UseSsl);
            });

            services.AddDbContext<ApplicationContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }

        public static async Task<IHost> MigrateUp(this IHost host)
        {
            using IServiceScope scope = host.Services.CreateScope();
            ApplicationContext db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            await db.Database.MigrateAsync();
            return host;
        }
    }
}

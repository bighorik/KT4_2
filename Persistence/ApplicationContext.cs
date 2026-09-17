using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ApplicationContext : DbContext
    {
        public DbSet<AttachmentEntity> Attachments { get; set; }
        public DbSet<InternetResourceLinkEntity> InternetResourceLinks { get; set; }
        public DbSet<MembershipApplicationEntity> MembershipApplications { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<ProjectMemberEntity> ProjectMember { get; set; }
        public DbSet<ProjectPositionEntity> ProjectPositions { get; set; }
        public DbSet<SkillEntity> Skills { get; set; }
        public DbSet<StudentGroupNumberEntity> StudentGroupNumbers { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserSkillEntity> UserSkills { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=pg4;Username=postgres;Password=admin");
        }
    }
}

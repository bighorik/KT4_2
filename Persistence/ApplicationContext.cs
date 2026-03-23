using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CategoryEntity> Categories { get; set; }

        public DbSet<ProductEntity> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=pg4;Username=postgres;Password=admin");
        }
    }
}

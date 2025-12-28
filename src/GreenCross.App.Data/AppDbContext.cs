using System.Reflection;
using GreenCross.App.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenCross.App.Data
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<FirstEntity> FirstEntities => Set<FirstEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Applies all IEntityTypeConfiguration<T> in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Optional: any small, cross-cutting conventions can go here
            // (avoid entity-specific config here; keep that in Configurations/*)
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            // Optional: global conventions (keep this minimal)
            // configurationBuilder.Properties<string>().HaveMaxLength(200);
            // configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }
    }
}

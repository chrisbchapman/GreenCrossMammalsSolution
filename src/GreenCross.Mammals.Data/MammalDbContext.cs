using System.Reflection;
using GreenCross.Mammals.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenCross.Mammals.Data
{
    public sealed class MammalDbContext : DbContext
    {
        public MammalDbContext(DbContextOptions<MammalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recorder> Recorders => Set<Recorder>();
        public DbSet<HarvestMouseRecord> HarvestMouseRecords => Set<HarvestMouseRecord>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<RecordVerificationStatus> RecordVerificationStatuses => Set<RecordVerificationStatus>();

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

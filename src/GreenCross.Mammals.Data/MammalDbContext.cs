using System.Reflection;
using GreenCross.Mammals.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenCross.Mammals.Data;

public sealed class MammalDbContext : DbContext
{
    private readonly string _currentUser;

    public MammalDbContext(DbContextOptions<MammalDbContext> options)
        : base(options)
    {
        // Set the current user - you can inject IHttpContextAccessor or another user provider
        // For now, using Environment.UserName as a fallback
        _currentUser = Environment.UserName ?? "System";
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
        configurationBuilder.Properties<string>().HaveMaxLength(30);
        // configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditFields()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is IEntity<int> &&
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (IEntity<int>)entry.Entity;
            var now = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = now;
                entity.CreatedBy = _currentUser;
            }

            if (entry.State == EntityState.Modified)
            {
                entity.UpdatedAt = now;
                entity.UpdatedBy = _currentUser;
            }
        }
    }
}

using GreenCross.Mammals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenCross.Mammals.Data.Configurations;

public class RecorderConfiguration : IEntityTypeConfiguration<Recorder>
{
    public void Configure(EntityTypeBuilder<Recorder> builder)
    {
        // Use RecorderId as the primary key
        builder.HasKey(e => e.RecorderId);

        // Configure RecorderId as identity column
        builder.Property(e => e.RecorderId)
            .ValueGeneratedOnAdd();

        // Ignore the Id property (it's just a proxy for IEntity<int>)
        builder.Ignore(e => e.Id);

        // Set default value for CreatedAt
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(e => e.Initials)
            .IsRequired()
            .HasMaxLength(4);
    }
}

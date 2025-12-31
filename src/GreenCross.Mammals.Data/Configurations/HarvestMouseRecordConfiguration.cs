using GreenCross.Mammals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenCross.Mammals.Data.Configurations;

public class HarvestMouseRecordConfiguration : IEntityTypeConfiguration<HarvestMouseRecord>
{
    public void Configure(EntityTypeBuilder<HarvestMouseRecord> builder)
    {
        // Use HarvestMouseRecordId as the primary key
        builder.HasKey(e => e.RecordId);

        // Configure HarvestMouseRecordId as identity column
        builder.Property(e => e.RecordId)
            .ValueGeneratedOnAdd();

        // Ignore the Id property (it's just a proxy for IEntity<int>)
        builder.Ignore(e => e.Id);

        // Set default value for CreatedAt
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");
    }

}

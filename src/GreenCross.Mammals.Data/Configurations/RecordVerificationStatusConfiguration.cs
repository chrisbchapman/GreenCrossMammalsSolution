using GreenCross.Mammals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenCross.Mammals.Data.Configurations;

public class RecordVerificationStatusConfiguration : IEntityTypeConfiguration<RecordVerificationStatus>
{
    public void Configure(EntityTypeBuilder<RecordVerificationStatus> builder)
    {
        // Use RecordVerificationStatusId as the primary key
        builder.HasKey(e => e.RecordVerificationStatusId);

        // Configure RecordVerificationStatusId as identity column
        builder.Property(e => e.RecordVerificationStatusId)
            .ValueGeneratedOnAdd();

        // Ignore the Id property (it's just a proxy for IEntity<int>)
        builder.Ignore(e => e.Id);

        // Set default value for CreatedAt
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");
    }

}

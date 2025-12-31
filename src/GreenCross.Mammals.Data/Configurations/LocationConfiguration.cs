using GreenCross.Mammals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenCross.Mammals.Data.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        // Use LocationId as the primary key
        builder.HasKey(e => e.LocationId);

        // Configure LocationId as identity column
        builder.Property(e => e.LocationId)
            .ValueGeneratedOnAdd();

        // Ignore the Id property (it's just a proxy for IEntity<int>)
        builder.Ignore(e => e.Id);

        // Set default value for CreatedAt
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        // Optional: Add constraints or indexes
        builder.Property(e => e.Easting)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(e => e.Northing)
            .HasMaxLength(10)
            .IsRequired();
    }
}

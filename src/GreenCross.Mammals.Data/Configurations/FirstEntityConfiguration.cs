using GreenCross.Mammals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenCross.Mammals.Data.Configurations;

public class FirstEntityConfiguration : IEntityTypeConfiguration<Recorder>
{
    public void Configure(EntityTypeBuilder<Recorder> builder)
    {
        builder.HasKey(e => e.RecorderId);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.ToTable("FirstEntities");
    }
}

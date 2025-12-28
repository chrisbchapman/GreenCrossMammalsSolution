using GreenCross.App.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenCross.App.Data.Configurations;

public class FirstEntityConfiguration : IEntityTypeConfiguration<FirstEntity>
{
    public void Configure(EntityTypeBuilder<FirstEntity> builder)
    {
        builder.HasKey(e => e.FirstEntityId);

        builder.Property(e => e.FirstEntityName)
            .IsRequired()
            .HasMaxLength(200);

        builder.ToTable("FirstEntities");
    }
}

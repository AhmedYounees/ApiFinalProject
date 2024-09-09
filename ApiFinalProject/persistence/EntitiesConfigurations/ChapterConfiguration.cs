

using ApiFinalProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiFinalProject.persistence.EntitiesConfigurations;

public class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
{
    public void Configure(EntityTypeBuilder<Chapter> builder)
    {
        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(c => c.Course)
            .WithMany(crs => crs.Chapters)
            .HasForeignKey(c => c.CourseId);

    }
}


using ApiFinalProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiFinalProject.persistence.EntitiesConfigurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.Property(r => r.Comment)
            .IsRequired()
            .HasMaxLength(1000);
        builder.HasOne(r => r.Course)
           .WithMany(c => c.Reviews)
           .HasForeignKey(r => r.CourseId);

        builder.HasOne(r => r.Student)
               .WithMany(s => s.Reviews)
               .HasForeignKey(r => r.StudentId);

        // Add unique constraint on CourseId and StudentId
        builder.HasIndex(r => new { r.CourseId, r.StudentId })
               .IsUnique();

    }
}


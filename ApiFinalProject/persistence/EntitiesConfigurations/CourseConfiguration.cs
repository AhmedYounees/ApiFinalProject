
using ApiFinalProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiFinalProject.persistence.EntitiesConfigurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired().HasMaxLength(500);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Courses)
            .HasForeignKey(x => x.CategoryId);

        builder.HasMany(x => x.Chapters)
            .WithOne(x => x.Course)
            .HasForeignKey(x => x.CourseId);

        builder.HasMany(c => c.Instructors)
        .WithMany(i => i.Courses)
            .UsingEntity<CourseInstructor>(
            j => j.HasOne(ci => ci.Instructor)
                  .WithMany(i => i.InstructorCourses)
                  .HasForeignKey(ci => ci.InstructorId),
            j => j.HasOne(ci => ci.Course)
                  .WithMany(c => c.CourseInstructors)
                  .HasForeignKey(ci => ci.CourseId),
            j => j.HasKey(ci => new { ci.CourseId, ci.InstructorId })
        );
    }
}
